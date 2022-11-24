@echo off
chcp 65001


echo ****************************************  
echo ===============协议导出=================
echo ****************************************  
echo .

protogen.exe  -i:testabc.proto    -o:testabc.cs



echo .
echo 导出完成!
pause 