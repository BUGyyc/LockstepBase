#! /usr/bin/env python
#coding=utf-8

import os
import sys
os.system('chcp 65001')
import xlrd

def getEnumStr(name, data):
    k_Tab = "   ";
    k_Enter = "\n"
    str = "namespace GameCore\n" \
          "{{\n" \
          "\tpublic enum {}\n" \
          "\t{{\n".format(name)
    for key, value in data.items():
        str += "\t\t{} = {},\n".format(key, value)
    str += "\t}\n" \
           "}"

    return str

def exportEnum(tableName, sheetName, valueCol, keyCol, filePath, enumName):
    print("###########exportEnum", tableName, sheetName, valueCol, keyCol, filePath, enumName)

    table = xlrd.open_workbook("./DataConfig/{}.xls".format(tableName))
    sheet = table.sheet_by_name(sheetName)
    enumData = {}
    for row in range(4, sheet.nrows):
        enumData[sheet.cell_value(row, int(keyCol))] = int(sheet.cell_value(row, int(valueCol)))

    assetPath = '../../oasis-anchang/Assets/'
    path = os.path.join(assetPath, filePath, '{}.cs'.format(enumName))
    with open(path, mode="w", encoding='utf-8') as f:
        f.write(getEnumStr(enumName, enumData))

if __name__ == '__main__' :
    if len(sys.argv) < 7 :
        sys.exit(-1)
    exportEnum(*sys.argv[1:])