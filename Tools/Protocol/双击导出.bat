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
echo 正在导出 pbclils.proto
tools\protoc.exe "pbclils.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional
echo .
echo 正在导出 pbbsls.proto
tools\protoc.exe "pbbsls.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional

echo .
echo 正在导出 proto_common.proto
tools\protoc.exe "proto_common.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional
echo . 
echo 正在导出 proto_pid.proto
tools\protoc.exe "proto_pid.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional
echo .
echo 正在导出 proto_room.proto
tools\protoc.exe "proto_room.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional
echo .
echo 正在导出 proto_battle.proto 
tools\protoc.exe "proto_battle.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional
echo .
echo 正在导出 proto_config.proto 
tools\protoc.exe "proto_config.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional

echo .
echo 正在导出 proto_editor.proto
tools\protoc.exe "proto_editor.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional






echo .
echo 导出完成!
pause 