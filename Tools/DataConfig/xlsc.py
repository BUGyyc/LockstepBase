#! /usr/bin/env python
#coding=utf-8

import os
import sys
import string
ERROR_LOG = "/tmp/parse_error.log"
if __name__ == '__main__' :
    XLS_NAME 	= sys.argv[1]
    SHEET_NAME 	= sys.argv[2]
    STEP1_XLS2PROTO_PATH = "step1_xls2proto"

    print "=========Compilation of %s.xls=========" %XLS_NAME

    os.chdir(STEP1_XLS2PROTO_PATH)
    print("current run dir : %s" %os.getcwd())
    print "TRY TO DELETE TEMP FILES IN STEP1:"

    def del_files_in_step1(dir,topdown=True):
        abs_dir = os.path.abspath(dir)
        for root, dirs, files in os.walk(dir, topdown):
            for name in files:
                if ((root == abs_dir) and (-1 != name.find("_pb2.py") or -1 != name.find("_pb2.pyc") or -1 != name.find(".proto") or -1 != name.find(".data") or -1 != name.find(".log") or -1 != name.find(".txt"))):
                    os.remove(name)
                    print("del file : %s" %name)
                    if root != abs_dir :
                        break

    del_files_in_step1(os.getcwd())
    print("del end")

    replaceConfigChinese2Key_command = os.path.join("python ..","replaceConfigChinese2Key.py" + " ..","DataConfig",XLS_NAME + ".xls ..","DataConfigLocalization ..","localization","dist","localization_dataconfig" + ".xls")
    print "command ====== " + replaceConfigChinese2Key_command
    os.system(replaceConfigChinese2Key_command)

    xls_deploy_tool_command = os.path.join("python xls_deploy_tool.py " + SHEET_NAME + " ..","DataConfig",XLS_NAME + ".xls")
    print "command ====== " + xls_deploy_tool_command
    deploy_ret = os.system(xls_deploy_tool_command)
    deploy_ret >>= 8


    if (0 != deploy_ret):
        print("before Open File")
        file = open(ERROR_LOG, 'w')
        file.write(xls_deploy_tool_command + " " + "Parse error.\n")
        file.close()
        print("Open File")
        print(xls_deploy_tool_command + " " + "Parse error.\n")
        sys.exit(deploy_ret)



#---------------------------------------------------
#  step2: proto->cs
#---------------------------------------------------
    os.chdir("..")
    print("step2 current run dir : %s" %os.getcwd())

    STEP2_PROTO2CS_PATH = os.path.join(".","step2_proto2cs")
    PROTO_DESC = "proto.protodesc"
    SRC_OUT = os.path.join("..","src")

    os.chdir(STEP2_PROTO2CS_PATH);
    print("current run dir : %s" %os.getcwd())
    print "TRY TO DELETE TEMP FILES in STEP2:"

    def del_files_in_step2(dir,topdown=True):
        for root, dirs, files in os.walk(dir, topdown):
            abs_dir = os.path.abspath(dir)
            for name in files:
                if ((root == abs_dir) and (-1 != name.find(".cs") or -1 != name.find(".protodesc") or -1 != name.find(".txt"))):
                    os.remove(name)
                    print("del file : %s" %name)
                    if root != abs_dir :
                        break

    del_files_in_step2(os.getcwd())
    print("del end")

    proto_file_list = []
    proto_dir = os.path.join("..",STEP1_XLS2PROTO_PATH)
    for root, dirs, files in os.walk(proto_dir, True):
        for name in files:
            if (-1 != name.find(".proto")):
                fileName,fileExt = os.path.splitext(os.path.basename(name))
                proto_file_list.append(fileName)
                print("proto file : %s" %fileName)

    for proto_file in proto_file_list:
        print proto_file
        protoc_command = os.path.join(".","protoc --descriptor_set_out=" + PROTO_DESC + " --proto_path=..",STEP1_XLS2PROTO_PATH + " ..",STEP1_XLS2PROTO_PATH,proto_file + ".proto")
        print "command ====== " + protoc_command
        os.system(protoc_command)

        protogen_command = os.path.join("mono ProtoGen","protogen.exe -i:" + PROTO_DESC + " -o:" + proto_file + ".cs")
        print "command ====== " + protogen_command
        protogen_ret = os.system(protogen_command)
        protogen_ret >>= 8;
        if (0 != protogen_ret):
            file = open(ERROR_LOG, 'w')
            file.write(protogen_command + " " + "Parse error.\n")
            file.close()
            print(protogen_command + " " + "Parse error.\n")
            sys.exit(protogen_ret)

    os.chdir("..")
    print("step2 current run dir : %s" %os.getcwd())

#---------------------------------------------------
#step3 : data and cs to assets dir
#---------------------------------------------------
    OUT_PATH=os.path.join("..","Assets")
    DATA_DEST=os.path.join("Resources","Common","DataConfig")
    CS_DEST=os.path.join("Scripts","Killer","DataConfig","ProtoGen")

    def CopyFileAndRename(srcFile,desFile):
        if(os.path.isfile(srcFile)):
            open(desFile,"wb").write(open(srcFile, "rb").read())

    def GetFileNameList(dir,ext,topdown=True):
        proto_file_list = []
        for root, dirs, files in os.walk(dir, topdown):
            for name in files:
                if (-1 != name.find(ext)):
                    fileName,fileExt = os.path.splitext(os.path.basename(name))
                    proto_file_list.append(fileName)
        return proto_file_list

    dataList = GetFileNameList(STEP1_XLS2PROTO_PATH,".data")
    for data in dataList :
        print(os.path.join(STEP1_XLS2PROTO_PATH,data + ".data -> ") + os.path.join(OUT_PATH,DATA_DEST,data + ".bytes"));
        CopyFileAndRename(os.path.join(STEP1_XLS2PROTO_PATH,data + ".data"),os.path.join(OUT_PATH,DATA_DEST,data + ".bytes"))
        csList = GetFileNameList(STEP2_PROTO2CS_PATH,".cs")
    for cs in csList :
        print(os.path.join(STEP2_PROTO2CS_PATH,cs + ".cs -> ") + os.path.join(OUT_PATH,CS_DEST,cs + ".cs"))
        CopyFileAndRename(os.path.join(STEP2_PROTO2CS_PATH,cs + ".cs"),os.path.join(OUT_PATH,CS_DEST,cs + ".cs"))


#---------------------------------------------------
#step4 : do del
#---------------------------------------------------

    print "TRY TO DEL TEMP FILE:"
    print "step1:"
    os.chdir(STEP1_XLS2PROTO_PATH)
    del_files_in_step1(os.getcwd())
    print "step2:"
    os.chdir("..")
    os.chdir(STEP2_PROTO2CS_PATH)
    del_files_in_step2(os.getcwd())
    os.chdir("..")

