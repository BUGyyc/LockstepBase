@echo off
chcp 65001

echo ****************************************  
echo ===============协议导出=================
echo ****************************************  
echo .

SET OUT_PATH=%cd%\..\..\LockstepClient\Assets\Shared\ProtocolDef

echo cs文件目录 %OUT_PATH%

echo .
echo 开始执行...

echo .
echo 正在导出 proto_common.proto
tools\ProtoGen.exe "proto_common.proto" -output_directory=%OUT_PATH% --proto_path=.\ --include_imports=%cd%
echo . 
echo 正在导出 proto_room.proto
tools\ProtoGen.exe "proto_room.proto" -output_directory=%OUT_PATH% --proto_path=.\ --include_imports=%cd%
echo .
echo 正在导出 proto_battle.proto 
tools\ProtoGen.exe "proto_battle.proto" -output_directory=%OUT_PATH% --proto_path=.\ --include_imports=%cd%
echo .
echo 正在导出 proto_config.proto 
tools\ProtoGen.exe "proto_config.proto" -output_directory=%OUT_PATH% --proto_path=.\ --include_imports=%cd%
echo . 
echo 正在导出 include\yd_fieldoptions.proto
tools\ProtoGen.exe "include\yd_fieldoptions.proto" -output_directory=%OUT_PATH% --proto_path=.\ --include_imports=%cd%
echo .
echo 正在导出 proto_datamodel.proto 
tools\ProtoGen.exe "proto_datamodel.proto" -output_directory=%OUT_PATH% --proto_path=.\ --include_imports=%cd% 
echo .
echo 正在导出 proto_avatar.proto
tools\ProtoGen.exe "proto_avatar.proto" -output_directory=%OUT_PATH% --proto_path=.\ --include_imports=%cd% 
echo .
echo 正在导出 proto_server.proto
tools\ProtoGen.exe "proto_server.proto" -output_directory=%OUT_PATH% --proto_path=.\ --include_imports=%cd% 
echo .
echo 正在导出 proto_editor.proto
tools\ProtoGen.exe "proto_editor.proto" -output_directory=%OUT_PATH% --proto_path=.\ --include_imports=%cd% 
echo .


echo .
echo 导出完成!
pause 