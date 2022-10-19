#coding=utf-8

import imp
import os
import sys
from os.path import dirname, basename, isfile
import glob
import importlib
from functools import partial

modules = glob.glob(dirname(os.path.realpath(__file__))+"/*_pb2.py*")

__all__ = [basename(f).split(".")[0] for f in modules if isfile(f) and not f.endswith('__init__.py') and not f.endswith("__init__.pyc")]


table_data_path = os.path.abspath(os.path.join(dirname(__file__), "../table_data"))

class DataCfgList(object):

    def __init__(self, pb_array_type, data_path):
        self.cfg_list = pb_array_type()
        with open(data_path, "rb") as f:
            self.cfg_list.ParseFromString(f.read()) 
        self.index = {}
        for cfg in self.cfg_list.items:
            self.index[cfg.cfg_id] = cfg

    def items(self):
        return self.cfg_list.items

    def get_cfg_by_xid(self, cfg_id):
        return self.index.get(cfg_id, None)
 

#if __name__ == "__main__":
my_module = importlib.import_module(__name__) 

def _get_cfg_func(data_cfg_name, xid):
    return my_module.__dict__[data_cfg_name].get_cfg_by_xid(xid)

def _get_cfg_list_func(data_cfg_name):
    return my_module.__dict__[data_cfg_name].items()


for f in modules:
    #f = m+"_pb2.py"
    #f = f + "_pb2.py" 

    if not os.path.exists(f):
        f += "c"
    
    if not isfile(f):
       continue 
    #if m == "__init__":
    #    continue
    if f.endswith('__init__.py') and f.endswith("__init__.pyc"):
       continue 
    #module = __import__(f) # importlib.import_module(m, __name__)
    #module = importlib.import_module(m)#, __name__)
    #print(f)
    module = imp.load_source(__name__ + "." + f.split(os.sep)[-1].split(".")[0], f)

    for key in module.__dict__.keys():
        if key.startswith("_"):
            continue
        if key.endswith("Cfg") or key.endswith("_ARRAY"): 
            cls = module.__dict__[key]
            my_module.__dict__[key] = cls

            if key.endswith("Cfg_ARRAY"):
                list_name = key.replace("Cfg_", "_Cfg_").lower().replace("cfg_array", "cfg_list")
                data_path = os.path.join(table_data_path, "%s.bytes"%(key.lower().replace("_array", "")))
      
                data_cfg_list = DataCfgList(cls, data_path)
                #data_cfg_list_dic[cls.__name__] = data_cfg_list
                my_module.__dict__[list_name] = data_cfg_list

                get_cfg_func_name = "get_%s"%(list_name.replace("_list", ""))
                get_cfg_list_func_name = "get_%s"%(list_name)
                my_module.__dict__[get_cfg_func_name] = partial(_get_cfg_func, list_name)
                my_module.__dict__[get_cfg_list_func_name] = partial(_get_cfg_list_func, list_name)


# cnt = 0
# data = {}
# for key in my_module.__dict__.keys():
#     if key.lower().__contains__("cfg"):
#         print(key)
#         data[key] = my_module.__dict__[key]
#         cnt += 1
# print(len(data), cnt)

for cfg in get_skill_cfg_list():
   print(cfg)

print(get_bag_cfg(2))
print(get_bag_cfg(3)) 


