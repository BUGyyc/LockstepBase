#coding=utf-8

import os
import sys

basedir = '../../../../oasis-anchang-server/trunk/src/assets/scripts'
clientdir = '../../oasis-anchang/Assets/Shared/Protocol/'

os.environ['SERVER_SRC_PATH'] = ""

sys.path.append(basedir)
sys.path.append(basedir+'/protocol')
sys.path.append(basedir+'/base')
sys.path.append(basedir+'/common')
sys.path.append(basedir+'/libs')


#if os.path.exists(os.path.join(basedir, "protocol/proto_code.py")):
#os.remove(os.path.join(basedir, "protocol/proto_code.py"))
pycode = '''
#coding=utf-8

from proto_datamodel_pb2 import *
from proto_avatar_pb2 import *
from proto_server_pb2 import *

class ProtoCode(object):
    pass

protos = {
}

funcs = {
}

def get_proto_type(code):
    return protos.get(code, None)

def is_valid_code(code):
    return code in protos

def get_func_name(code):
    return funcs.get(code, None)

def get_code_by_func(func_name):
    for code, func in funcs.items():
        if func == func_name:
            return code
    return None

'''

with open(os.path.join(basedir, 'protocol/proto_code.py'), 'w', encoding="utf-8") as f:
    f.write(pycode)

import os
import hashlib

import protocol.proto_avatar_pb2 as proto_avatar
import protocol.proto_server_pb2 as proto_server

protos = [proto_avatar, proto_server]

from types import ModuleType

class fake_cls(object):
    pass

v_types_module = ModuleType("Types")
sys.modules["Types"] = v_types_module

v_kbe_module = ModuleType('KBEngine')
v_kbe_module.globalData = {}
v_kbe_module.Proxy = fake_cls
v_kbe_module.Entity = fake_cls
v_kbe_module.scriptLogType = lambda x: None
v_kbe_module.LOG_TYPE_ERR = 1
#v_kbe_module.user_type_dict = {}
sys.modules['KBEngine'] = v_kbe_module

def fake_func(*args, **kwargs): 
    def internal(func): 
        return func
    return internal

v_def_module = ModuleType('EntityDef') 
v_def_module.rename = fake_func
v_def_module.interface = fake_func
v_def_module.method = fake_func
v_def_module.clientmethod = fake_func
v_def_module.property = fake_func
v_def_module.entity = fake_func
v_def_module.INT32 = int
v_def_module.UINT32 = int
v_def_module.UINT64 = int
v_def_module.INT64 = int
v_def_module.INT8 = int
v_def_module.UNICODE = str
v_def_module.BLOB = type(b'')
v_def_module.BASE = 1
v_def_module.BASE_AND_CLIENT = 2
v_def_module.persistent = False
sys.modules['EntityDef'] = v_def_module

import types


from base.Avatar import Avatar

for k, v in Avatar.__dict__.items():
    if k.startswith("__"):
        continue
    import rpc_util
    if rpc_util.is_rpc_method(v):
        print(k, v.code)

LONG_UPPER_BOUND = 18446744073709551615

def gen_long_hash_from_str(name):
    md5 = int(hashlib.md5(name.encode('utf-8')).hexdigest(), 16)
    md5str = str(md5)
    src = md5.to_bytes(int(len(md5str)/2), byteorder=sys.byteorder)

    buffer = bytearray(8)
    buffer[:] = src[0:8]
    long1 = int.from_bytes(buffer, byteorder=sys.byteorder)
    buffer[:] = src[8:]
    long2 = int.from_bytes(buffer, byteorder=sys.byteorder)
    #return LGUID.new_long_guid(long1, long2)
    m = 0xc6a4a7935bd1e995
    h = m >> 4 & LONG_UPPER_BOUND
    r = 47

    k = long1

    k = (k*m) & LONG_UPPER_BOUND
    k ^= ((k >> r) & LONG_UPPER_BOUND)
    k = (k*m)& LONG_UPPER_BOUND

    h ^= (k & LONG_UPPER_BOUND)
    h = (h*m)& LONG_UPPER_BOUND

    k = long2

    k = (k*m) & LONG_UPPER_BOUND
    k ^= ((k >> r) & LONG_UPPER_BOUND)
    k = (k*m) & LONG_UPPER_BOUND

    h ^= (k & LONG_UPPER_BOUND)
    h = (h*m)& LONG_UPPER_BOUND

    h ^= ((h >> r) & LONG_UPPER_BOUND)
    h = (h*m) & LONG_UPPER_BOUND
    h ^= ((h >> r) & LONG_UPPER_BOUND)

    return ((h >> r)& 0xffffffff) ^ (h & 0xffffffff)

    #return h

def format_code(text):
    return ''.join(' ' + char if char.isupper() else char.strip() for char in text).strip().upper().replace(' ', '_')


def gen_py_code(): 
    pycode = '''
#coding=utf-8

from proto_datamodel_pb2 import *
from proto_avatar_pb2 import *
from proto_server_pb2 import *

class ProtoCode(object):
'''
    pycode2 = '''
protos = {'''

    pycode3 = '''
funcs = {
'''
    req_list = []
    rsp_list = []
    for proto in protos:
        for name, desc in proto.DESCRIPTOR.message_types_by_name.items():
            if name.endswith("Req"):
                req_list.append("%s"%(format_code(name)))
                #req_list.append('''%s = "%s"'''%(format_code(name), format_code(name).lower()))
            elif name.endswith("Rsp") or name.endswith("Ntf"):
                #rsp_list.append('''%s = "%s"'''%(format_code(name), format_code(name).lower()))
                rsp_list.append("%s"%(format_code(name)))
            if name.endswith("Req") or name.endswith("Rsp") or name.endswith("Ntf"):
        #         pycode2 += '''    
        # if flag == ProtoCode.%s:  
        #     return %s'''%(format_code(name), name) 
                pycode2 += '''
        ProtoCode.%s: %s,'''%(format_code(name), name)

    final_list = sorted(req_list+rsp_list)

    for idx, name in enumerate(final_list):
        pid = gen_long_hash_from_str(name)
        pycode += '    %s = %s\n'%(name, pid)
        
        if name.endswith("_REQ"):
            pycode3 += '    ProtoCode.%s: \"%s\",\n'%(name, name.lower()[0:-4])
        elif name.endswith("_RSP") or name.endswith("_NTF"):
            pycode3 += '    ProtoCode.%s: \"%s\",\n'%(name, "client_on_" + name.lower()[0:-4])

    pycode += pycode2
    pycode += '''\n}\n'''
    pycode += pycode3
    pycode += '''}

def get_proto_type(code):
    return protos.get(code, None)

def is_valid_code(code):
    return code in protos

def get_func_name(code):
    return funcs.get(code, None)

def get_code_by_func(func_name):
    for code, func in funcs.items():
        if func == func_name:
            return code
    return None

'''
    print(pycode)
    with open(os.path.join(basedir, 'protocol/proto_code.py'), 'w', encoding="utf-8") as f:
        f.write(pycode)

def gen_cs_code():

    for proto in protos:
        cs_file_name = proto.DESCRIPTOR.name.split(".")[0].replace("proto_", "").capitalize()
        cscode = '''
#if KBE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.%s
{ 
    public static class ProtoCode
    {
'''%(cs_file_name)
        req_list = []
        rsp_list = []
        for name, desc in proto.DESCRIPTOR.message_types_by_name.items():
            if name.endswith("Req"):
                req_list.append("%s"%(format_code(name)))
                #req_list.append('''public static const UInt32 %s = "%s";'''%(format_code(name), format_code(name).lower()))
            elif name.endswith("Rsp") or name.endswith("Ntf"):
                req_list.append("%s"%(format_code(name)))
                #rsp_list.append('''public static const UInt32 %s = "%s";'''%(format_code(name), format_code(name).lower()))

        final_list = sorted(req_list+rsp_list)
        for idx, name in enumerate(final_list):
            pid = gen_long_hash_from_str(name)
            cscode += '        public const UInt32 %s = %s;\n'%(name, pid) 

        cscode += '''
    }
}
#endif
    '''
        print(cscode)
        with open(os.path.join(clientdir, 'ProtoCode.%s.cs'%(cs_file_name)), 'w', encoding="utf-8") as f:
            f.write(cscode)

class CppType(object):
    CPPTYPE_INT32       = 1
    CPPTYPE_INT64       = 2
    CPPTYPE_UINT32      = 3
    CPPTYPE_UINT64      = 4
    CPPTYPE_DOUBLE      = 5
    CPPTYPE_FLOAT       = 6
    CPPTYPE_BOOL        = 7
    CPPTYPE_ENUM        = 8
    CPPTYPE_STRING      = 9
    CPPTYPE_MESSAGE     = 10

def cpp_to_cstype(label, cpp_type, msg_type, enum_type): 
    if cpp_type == CppType.CPPTYPE_INT32:
        return "List<Int32>" if label == 3 else "Int32"
    elif cpp_type == CppType.CPPTYPE_INT64:
        return "List<Int64>" if label == 3 else "Int64"
    elif cpp_type == CppType.CPPTYPE_UINT32:
        return "List<UInt32>" if label == 3 else "UInt32"
    elif cpp_type == CppType.CPPTYPE_UINT64:
        return "List<UInt64>" if label == 3 else "UInt64"
    elif cpp_type == CppType.CPPTYPE_DOUBLE:
        return "List<Double>" if label == 3 else "Double"
    elif cpp_type == CppType.CPPTYPE_FLOAT:
        return "List<Single>" if label == 3 else "Single"
    elif cpp_type == CppType.CPPTYPE_BOOL:
        return "List<bool>" if label == 3 else "bool"
    elif cpp_type == CppType.CPPTYPE_ENUM:
        return "List<%s>"%(enum_type.name) if label == 3 else enum_type.name
    elif cpp_type == CppType.CPPTYPE_STRING:
        return "List<string>" if label == 3 else "string"
    elif cpp_type == CppType.CPPTYPE_MESSAGE:
        return "List<%s>"%(msg_type.name) if label == 3 else msg_type.name
    return ""

from user_type import *
from user_type import types_all

def gen_comment(tp):
    def gen(flds, space):
        code = ""
        for fld in flds:
            code += "%s%s:%s, \n"%(' '*space, fld.name, cpp_to_cstype(fld.label, fld.cpp_type, fld.message_type, fld.enum_type))
            if fld.cpp_type == 10:
                dm = types_all.get_data_model(fld.message_type._concrete_class)
                #print("XXXXXXXXXXXXXXX", dm, fld.message_type._concrete_class.__name__)
                if dm is not None:
                    code = code.replace(cpp_to_cstype(fld.label, fld.cpp_type, fld.message_type, fld.enum_type), dm.__name__)
        return code
    fields = []
    cb_fields = None
    for field in tp.DESCRIPTOR.fields: 
        if field.name == "callback":
            cb_fields = field.message_type._concrete_class.DESCRIPTOR.fields
            continue
        fields.append(field)
    result = ""
    if cb_fields is not None:
        result = " %s    callback=(\n%s    )"%(gen(fields, space=4), gen(cb_fields, space=8))
    else:
        result = " %s"%(gen(fields, space=4))
    if result.endswith(", "):
        result = result[0:-2]
    if result.startswith(", "):
        result = result[2:]
    if result.endswith(", )"):
        result = result[0:-3] + ")"
    if result.startswith("  "):
        result = result[1:]
    return result

def format_cs_var(name):
    return ''.join([part.capitalize() for part in name.split('_')])

def gen_assign_code(field):
    assignment = ''   
    if field.label == 3:
        if field.cpp_type == 10:
            assignment += '''
                foreach(var item in %s)
                {
                    if(item == null)
                    {
                        var data = new %s();
                        data.Oid = 0;
                        obj.%s.Add(item);
                    }
                    else
                    {
                        obj.%s.Add(item);
                    } 
                }
'''%(
field.name, 
cpp_to_cstype(1, field.cpp_type, field.message_type, field.enum_type),
format_cs_var(field.name), 
format_cs_var(field.name)
)
        else:
            assignment += "            foreach(var item in %s)\n            {\n                req.Add%s(item);\n            }\n"%(field.name, format_cs_var(field.name))
    else:
        if field.cpp_type == 10:
            assignment += '''
                if (%s == null)
                {
                    obj.%s = new %s();
                    obj.%s.Oid = 0;
                }
                else
                {
                    obj.%s = %s;
                }
'''%(
field.name, format_cs_var(field.name), 
cpp_to_cstype(field.label, field.cpp_type, field.message_type, field.enum_type),
format_cs_var(field.name), 
format_cs_var(field.name), 
field.name
)
        else:
            assignment += "                obj.%s = %s;\n"%(format_cs_var(field.name), field.name);
    return assignment

def gen_rpc_stub(): 

    pycode = '''#coding=utf-8

#This file is auto-generated
#Do not modifiy it! 

import queue

from rpc_util import server_api, client_api
from proto_code import ProtoCode

from log import Log
logger = Log.get_logger("rpc_module")

class RpcModule(object):

    def __init__(self):
        self.rpc_entities = {}
        self.rpc_partial_data = {}
        self.rpc_process_queue = queue.Queue()
        self.add_repeat_timer(0, 0.1, self.process_rpc)

    def on_client_disconnect(self):
        try:
            self.rpc_process_queue.get()
        except:
            pass

    def __reload__(self):
         pass
 
    def process_rpc(self):
        for i in range(10):
            if self.rpc_process_queue.empty():
                return
            top = self.rpc_process_queue.get_nowait()
            top()

    \'\'\'
'''
    from protocol import proto_code
    
    for proto in protos:
        for name, desc in proto.DESCRIPTOR.message_types_by_name.items():
            if name.endswith("Req"):
                rpc_name = format_code(name).lower()[0: -4] #.replace("_req", "").replace("_ntf", "")
                flag = format_code(name) 

                code = getattr(proto_code.ProtoCode, flag)
     
                #flag_value = flag.lower()
                args_code = '' 
                tp = proto_code.get_proto_type(code)
                for field in tp().DESCRIPTOR.fields: 
                    args_code += field.name + ", "
                if args_code.endswith(", "):
                    args_code = args_code[0:-2]
                rpc_code = '''
%s
    @server_api() 
    def %s(self, %s):
        pass
'''%(gen_comment(tp), rpc_name, args_code)
                if args_code == "":
                    rpc_code = rpc_code.replace("(self, ", "(self")
                pycode += rpc_code
            elif name.endswith("Rsp") or name.endswith("Ntf"):
                rpc_name = format_code(name).lower()[0: -4] #.replace("_rsp", "")
                flag = format_code(name)
                code = getattr(proto_code.ProtoCode, flag)
                args_code = ''
                tp = proto_code.get_proto_type(code)
                for field in tp().DESCRIPTOR.fields: 
                    args_code += field.name + ", "
                if args_code.endswith(", "):
                    args_code = args_code[0:-2] 
                rpc_code = '''
%s
    @client_api()
    def client_on_%s(self, %s):
        pass
'''%(gen_comment(tp), rpc_name, args_code)
                if args_code == "":
                    rpc_code = rpc_code.replace("(self, ", "(self")
                pycode += rpc_code

        #print(pycode)
    pycode += "\n    '''"
    with open(os.path.join(basedir, "umodule/rpc_module.py"), "w", encoding="utf-8") as f:
        f.write(pycode)


    
    for proto in protos:
        cs_file_name = proto.DESCRIPTOR.name.split(".")[0].replace("proto_", "").capitalize()
        cscode = '''
//AUTOGEN code
//Do not modify!

#if KBE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Protocol;
using Protocol.%s;

namespace KBEngine
{
    public partial class Avatar
    {'''%(cs_file_name)
        
        for name, desc in proto.DESCRIPTOR.message_types_by_name.items():
            if name.endswith("Req"):
                rpc_name = format_code(name).lower()[0: -4] #.replace("_req", "")
                flag = format_code(name) 
                code = getattr(proto_code.ProtoCode, flag)

                has_cb = False
     
                args_code = '' 
                tp = proto_code.get_proto_type(code)
                param_code = ''
                assignment = ''
                cb_args = '' 
                cb_parse_code = ""
                for field in tp().DESCRIPTOR.fields:
                    print(field.name, field.cpp_type, cpp_to_cstype(field.label, field.cpp_type, field.message_type, field.enum_type))
                    
                    if field.name == 'callback':
                        has_cb = True
                        cb_arg_types = ""
                        
                        for cb_fld in field.message_type._concrete_class.DESCRIPTOR.fields:
                            print("SEKKIT", cb_fld.name, cpp_to_cstype(cb_fld.label, cb_fld.cpp_type, cb_fld.message_type, cb_fld.enum_type) + ", ")
                            cb_arg_types += cpp_to_cstype(cb_fld.label, cb_fld.cpp_type, cb_fld.message_type, cb_fld.enum_type) + ", "
                            if cb_fld.label == 3: #LIST
                                if cb_fld.cpp_type == 10: #MessageType
                                    cb_parse_code += '''
                    var cb_p%s = cb.%s.ToList();
                    for(int i=0;i<cb.%s.Count;++i)
                        //if(cb.%s[i].Oid == 0)
                        if(RpcUtil.IsNone(cb.%s[i]))
                            cb_p%s[i] = null; 
'''%(cb_fld.index, format_cs_var(cb_fld.name), format_cs_var(cb_fld.name), format_cs_var(cb_fld.name), format_cs_var(cb_fld.name), cb_fld.index)
                                    cb_args += "cb_p%s, "%(cb_fld.index);
                                else:
                                    cb_args += "cb.%s.ToList(), "%(format_cs_var(cb_fld.name));
                            else:
                                if cb_fld.cpp_type == 10: #MessageType
                                    cb_args += "//cb.%s.Oid==0?null:cb.%s, \n"%(format_cs_var(cb_fld.name), format_cs_var(cb_fld.name))
                                    cb_args += "                RpcUtil.IsNone(cb.%s)?null:cb.%s, "%(format_cs_var(cb_fld.name), format_cs_var(cb_fld.name))
                                else:
                                    cb_args += "cb.%s, "%(format_cs_var(cb_fld.name));
                        if cb_arg_types.endswith(", "):
                            cb_arg_types = cb_arg_types[0:-2]
                        param_code += "Action<%s> callback"%(cb_arg_types)
                        continue
                    args_code += field.name + ", "
                    param_code += cpp_to_cstype(field.label, field.cpp_type, field.message_type, field.enum_type) + " " + field.name + ", "
                    if field.label == 3:
                        if field.cpp_type == 10:
                            assignment += '''
            if(%s != null)
            foreach(var item in %s)
            {
                if(item == null)
                {
                    var data = new %s();
                    //data.Oid = 0;
                    req.Add%s(item);
                }
                else
                {
                    req.Add%s(item);
                } 
            }
'''%(
    field.name,
    field.name, 
    cpp_to_cstype(1, field.cpp_type, field.message_type, field.enum_type),
    format_cs_var(field.name), 
    format_cs_var(field.name)
)
                        else:
                            assignment += "            if(%s != null)\n            foreach(var item in %s)\n            {\n                req.Add%s(item);\n            }\n"%(field.name, field.name, format_cs_var(field.name))
                    else:
                        if field.cpp_type == 10:
                            assignment += '''
            if (%s == null)
            {
                req.%s = new %s();
                //req.%s.Oid = 0;
            }
            else
            {
                req.%s = %s;
            }
'''%(
        field.name, format_cs_var(field.name), 
        cpp_to_cstype(field.label, field.cpp_type, field.message_type, field.enum_type),
        format_cs_var(field.name), 
        format_cs_var(field.name), 
        field.name
    )
                        else:
                            assignment += "            req.%s = %s;\n"%(format_cs_var(field.name), field.name);
                if param_code.endswith(", "):
                    param_code = param_code[0:-2]
                if args_code.endswith(", "):
                    args_code = args_code[0:-2]
                if cb_args.endswith(", "):
                    cb_args = cb_args[0:-2]
                if param_code.__contains__("Action<>"):
                    param_code = param_code.replace("Action<>", "Action")
                if has_cb:
                    rpc_code = '''
        [Request(ProtoCode.%s)]
        public void rpc_%s(%s)
        {
            var req = new Protocol.%s();
%s            //if(callback == null)
                //this.Rpc(ProtoCode.%s, req.ToByteArray(), null);
            //else
            this.Rpc(ProtoCode.%s, req.ToByteArray(), (bytes) =>
            {
                if(callback!=null)
                {
                    var cb = Protocol.%s.Types.Callback.ParseFrom(bytes);
                    %s
                    callback(%s);
                }
            }); 
        }
            '''%(flag, rpc_name, param_code, name, assignment,
                flag, flag, name, cb_parse_code, cb_args
                )
                    if cb_args == "":
                        rpc_code = rpc_code.replace("var cb = Protocol.", "//var cb = Protocol.")
                    cscode += rpc_code
                else:
                    rpc_code = '''
        [Request(ProtoCode.%s)]
        public void rpc_%s(%s)
        {
            var req = new Protocol.%s();
%s            this.Rpc(ProtoCode.%s, req.ToByteArray(), null);
        }
'''%(flag, rpc_name, param_code, name, assignment, flag)
                    if cb_args == "":
                        rpc_code = rpc_code.replace("var cb = Protocol.", "//var cb = Protocol.")
                    cscode += rpc_code
            elif name.endswith("Rsp") or name.endswith("Ntf"):
                rpc_name = format_code(name).lower()[0: -4] #.replace("_rsp", "")
                flag = format_code(name)
                code = getattr(proto_code.ProtoCode, flag)
                #flag_value = flag.lower()
                args_code = ''
                param_code = ''
                type_code = ''
                parse_code = ''
                has_cb = False
                cb_assign = ''
                cb_args = ''
                cb_arg_types = ''
                tp = proto_code.get_proto_type(code)
                for idx, field in enumerate(tp().DESCRIPTOR.fields): 
                    print(field.type, field.message_type, field.cpp_type)

                    if field.name == 'callback':
                        has_cb = True
                        cb_arg_types = ""
                        for cb_fld in field.message_type._concrete_class.DESCRIPTOR.fields:
                            print("SEKKIT", cb_fld.name, cpp_to_cstype(cb_fld.label, cb_fld.cpp_type, cb_fld.message_type, cb_fld.enum_type) + ", ")
                            cb_arg_types += cpp_to_cstype(cb_fld.label, cb_fld.cpp_type, cb_fld.message_type, cb_fld.enum_type) + ", "
                            cb_args += "%s, "%(cb_fld.name);
                            cb_assign += gen_assign_code(cb_fld)

                        if cb_arg_types.endswith(", "):
                            cb_arg_types = cb_arg_types[0:-2]

                        continue

                    if field.label == 3:
                        if field.cpp_type == 10: #MessageType
                            args_code += "param%s"%(idx)
                            parse_code += \
            '''
            var param%s = new %s();
            foreach (var item in rsp.%s)
                //param%s.Add(item.Oid==0?null:item);
                param%s.Add(RpcUtil.IsNone(item)?null:item);
'''%(idx, cpp_to_cstype(field.label, field.cpp_type, field.message_type, field.enum_type),
                            format_cs_var(field.name),
                            idx, idx)
                        else:
                            args_code += "rsp."+format_cs_var(field.name) + ".ToList(), "
                    else:
                        if field.cpp_type == 10: #MessageType
                            args_code += "//rsp.%s.Oid==0?null:rsp.%s, \n"%(format_cs_var(field.name), format_cs_var(field.name)) 
                            args_code += "               RpcUtil.IsNone(rsp.%s)?null:rsp.%s, "%(format_cs_var(field.name), format_cs_var(field.name)) 
                        else:
                            args_code += "rsp." + format_cs_var(field.name) + ", "
                    type_code += cpp_to_cstype(field.label, field.cpp_type, field.message_type, field.enum_type)+", "
                    param_code += cpp_to_cstype(field.label, field.cpp_type, field.message_type, field.enum_type) + " " + field.name + ", "
                if param_code.endswith(", "):
                    param_code = param_code[0:-2]
                if type_code.endswith(", "):
                    type_code = type_code[0:-2]
                if args_code.endswith(", "):
                    args_code = args_code[0:-2]
                if cb_args.endswith(", "):
                    cb_args = cb_args[0:-2] 
                if not has_cb:
                    rpc_code = ''' 
        [Response(ProtoCode.%s)]
        protected void client_on_%s(byte[] param)
        {
            var rsp = %s.ParseFrom(param);
%s            on_%s?.Invoke(%s);
        }
        public event Action<%s> on_%s;'''%(flag, rpc_name, name, parse_code, rpc_name, args_code, type_code, rpc_name)
                else:
                    rpc_code = ''' 
        [Response(ProtoCode.%s)]
        protected void client_on_%s(UInt64 protocolId, byte[] param)
        {
            var rsp = %s.ParseFrom(param);
%s            on_%s?.Invoke(%s, (%s) => {
                var obj = new %s.Types.Callback();
%s                this.RpcCallback(protocolId, ProtoCode.%s, obj.ToByteArray());
            });
        }
        public event Action<%s, Action<%s>> on_%s;'''%(
                flag, rpc_name, name, 
                parse_code, rpc_name, args_code,
                cb_args, name, cb_assign, flag,
                type_code, cb_arg_types, rpc_name)
                
                if args_code == "":
                    rpc_code = rpc_code.replace("Action<>", "Action")
                rpc_code = rpc_code.replace("Action<, ", "Action<")
                rpc_code = rpc_code.replace("?.Invoke(, ", "?.Invoke(")

                cscode += rpc_code

        cscode += '''
    }
}

#endif
'''
        #print(cscode) 
        csfile = os.path.join(clientdir, "../Model/Entities/Avatar.Rpc.%s.cs"%(cs_file_name))
        with open(csfile, "w", encoding="utf-8") as f:
            f.write(cscode)


gen_py_code()
gen_cs_code()

gen_rpc_stub()




