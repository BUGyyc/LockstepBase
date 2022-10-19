#! /usr/bin/env python
#coding=utf-8

import os
os.system('chcp 65001')
import xlrd

bat_out_dir = "./bat_out/"
cs_out_dir = "../../oasis-anchang/Assets/Shared/DataConfigDef/"

#获取目录下的所有文件信息
file_list = os.popen(r"dir .\DataConfig /b").readlines()

f_len = len(file_list)

#表格列表
all_sheel_list = []

for i in range(f_len):

    #判断是不是xls文件
    if len(file_list[i]) > 5 and file_list[i][-5:-1] == '.xls':
        
        #打开xls文件
        xl = xlrd.open_workbook("./DataConfig/" + file_list[i][:-1])
        
        #获取xls文件的所有表
        sheets = xl.sheet_names()
        
        #创建表格执行文件
        file_path = bat_out_dir + file_list[i][:-5] + ".bat"
        
        #创建单个表格执行文件
        sheel_bat = open(file_path,"w")
        
        sheel_bat.write('chcp 65001\n')
        
        for ii in range(len(sheets)):
            if(sheets[ii][0] != '#'):
                cmd = "call xlsc.bat " + file_list[i][:-5] + " " + sheets[ii]
                
                #进入根目录
                sheel_bat.write("cd .. \n")
                sheel_bat.write(cmd + "\n")
                #添加表文件
                all_sheel_list.append(sheets[ii])
                
                cmd = "@" + cmd
                
                #执行命令
                os.system(cmd)
                
        if file_path == './bat_out/Tag.bat':
            sheel_bat.write('cd ..\n')
            sheel_bat.write('python exportEnum.py Tag Tag 0 1 GameCore/Entity/Entity TagDefine\n')
            sheel_bat.write('python exportEnum.py Tag Event 0 1 GameCore/Entity/Entity EventDefine\n')
        if file_path == './bat_out/Talent.bat':
            sheel_bat.write('cd ..\n')
            sheel_bat.write('python exportEnum.py Talent Talent 0 1 GameCore/Entity/Entity TalentDefine\n')
        sheel_bat.write('pause')
        sheel_bat.close()
        
    #打开模板文件
    cs_file = open('./template/DataConfigList.cs','r')
    cs_template_str = cs_file.read()
    cs_file.close()
    
    startElementIndex = cs_template_str.find('//Add Element')
    startGetMethodIndex = cs_template_str.find('//Add GetMethods')
    
if startElementIndex > 0 and startGetMethodIndex > 0:
    startElementIndex += 13
        
    #创建cs文件
    cs_file = open(cs_out_dir + 'DataConfigList.cs', 'w')
    cs_file.write(cs_template_str[:startElementIndex])
        
    sheel_len = len(all_sheel_list)
        
    print("表格数量:" + str(sheel_len))
    
    #加入类型
    for i in range(sheel_len):
        cs_file.write('\n \t\t\t_configTypeDic[typeof(' + all_sheel_list[i] + 'CfgSheet)] = ' + all_sheel_list[i] + 'CfgSheet.Parser;')
        
    startGetMethodIndex += 16
    
    #加入GET_BY_ID方法
    cs_file.write('\n' + cs_template_str[startElementIndex+1:startGetMethodIndex])
    
    for i in range(sheel_len):
        cs_file.write('\n \t\tpublic static ' + all_sheel_list[i] + 'Cfg Get' +all_sheel_list[i] + 'CfgbyId(uint id){')
        cs_file.write('\n \t\t\tvar dic = ConfigManager.instance.GetDataList<'+all_sheel_list[i]+'CfgSheet>();')
        cs_file.write('\n \t\t\t' + all_sheel_list[i] + 'Cfg item = null;')
        cs_file.write('\n \t\t\tif(dic != null){')
        cs_file.write('\n \t\t\t\tdic.ItemDic.TryGetValue(id, out item);')
        cs_file.write('\n \t\t\t}')
        cs_file.write('\n \t\t\treturn item;')
        cs_file.write('\n \t\t}')
        cs_file.write('\n')
        
        cs_file.write('\n \t\tpublic static IDictionary<uint, ' + all_sheel_list[i] + 'Cfg> ' +all_sheel_list[i] + 'CfgDic{')
        cs_file.write('\n \t\t\tget')
        cs_file.write('\n \t\t\t{')
        cs_file.write('\n \t\t\t\tvar dic = ConfigManager.instance.GetDataList<'+all_sheel_list[i]+'CfgSheet>();')
        cs_file.write('\n \t\t\t\tif(dic != null){ return dic.ItemDic; }')
        cs_file.write('\n \t\t\t\treturn null;')
        cs_file.write('\n \t\t\t}')
        cs_file.write('\n \t\t}')
        cs_file.write('\n')
    
    cs_file.write('\n' + cs_template_str[startGetMethodIndex+1:])
    cs_file.close()
        
    print('导出完成')
else:
    print('找不到插入标记，请添加//Add Element 以及//Add GetMethods')
        


    
    


