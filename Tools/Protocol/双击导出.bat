@echo off
chcp 65001

echo ****************************************  
echo ===============协议导出=================
echo ****************************************  
echo .

SET OUT_PATH=%cd%\..\..\oasis-anchang\Assets\Shared\ProtocolDef
SET OUT_C_PATH=%cd%\..\..\oasis-anchang\Assets\Shared\ProtocolDef-c
SET PYTHON_PATH=%cd%\..\..\..\oasis-aiclient\pyproj\protocol
SET SERVER_PATH=%cd%\..\..\..\..\oasis-anchang-server\trunk\src\assets\scripts\protocol
SET PYTHON_TEST_PATH=%cd%\..\PyTest\pb
SET PYTHON_TEST_OUT_PATH=%cd%\..\MockAIServer
SET PYTHON_TEST_C_OUT_PATH=%cd%\..\PyTest\Cs

echo cs文件目录 %OUT_PATH%
echo py文件目录 %PYTHON_PATH%
echo server_py文件目录 %SERVER_PATH%
echo python自动化测试pb目录 %PYTHON_TEST_PATH%
echo python自动化测试导出 %PYTHON_TEST_OUT_PATH%

echo .
echo 开始执行...


if exist %PYTHON_TEST_PATH% (
copy proto_common.proto %PYTHON_TEST_PATH%\
copy proto_pid.proto %PYTHON_TEST_PATH%\
copy proto_room.proto %PYTHON_TEST_PATH%\
copy proto_battle.proto %PYTHON_TEST_PATH%\
copy proto_config.proto %PYTHON_TEST_PATH%\
copy proto_record.proto %PYTHON_TEST_PATH%\
) else  (
	echo python自动化测试pb目录
)

echo .
echo 正在导出 pbclils.proto
tools\protoc.exe "pbclils.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional --python_out=%PYTHON_TEST_OUT_PATH%
echo .
echo 正在导出 pbbsls.proto
tools\protoc.exe "pbbsls.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional --python_out=%PYTHON_TEST_OUT_PATH%

echo .
echo 正在导出 proto_common.proto
tools\protoc.exe "proto_common.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional --python_out=%PYTHON_TEST_OUT_PATH%
echo . 
echo 正在导出 proto_pid.proto
tools\protoc.exe "proto_pid.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional --python_out=%PYTHON_TEST_OUT_PATH%
echo .
echo 正在导出 proto_room.proto
tools\protoc.exe "proto_room.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional --python_out=%PYTHON_TEST_OUT_PATH%
echo .
echo 正在导出 proto_battle.proto 
tools\protoc.exe "proto_battle.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional --python_out=%PYTHON_TEST_OUT_PATH%
echo .
echo 正在导出 proto_config.proto 
tools\protoc.exe "proto_config.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional --python_out=%PYTHON_TEST_OUT_PATH%

echo .
echo 正在导出 proto_editor.proto
tools\protoc.exe "proto_editor.proto" --csharp_out=%OUT_PATH% --experimental_allow_proto3_optional --python_out=%PYTHON_TEST_OUT_PATH%


if exist %PYTHON_PATH% (
copy proto_common.proto %PYTHON_PATH%\..\..\proto\
copy proto_pid.proto %PYTHON_PATH%\..\..\proto\
copy proto_room.proto %PYTHON_PATH%\..\..\proto\
copy proto_battle.proto %PYTHON_PATH%\..\..\proto\
copy proto_config.proto %PYTHON_PATH%\..\..\proto\
copy proto_record.proto %PYTHON_PATH%\..\..\proto\
) else  (
	echo py目录不存在
)

if exist %SERVER_PATH% (

tools\protoc.exe "proto_datamodel.proto" --proto_path=.\ --include_imports=%cd% --python_out=%SERVER_PATH%
tools\protoc.exe "proto_common.proto" --proto_path=.\ --include_imports=%cd% --python_out=%SERVER_PATH%
tools\protoc.exe "proto_pid.proto" --proto_path=.\ --include_imports=%cd% --python_out=%SERVER_PATH%
tools\protoc.exe "proto_config.proto" --proto_path=.\ --include_imports=%cd% --python_out=%SERVER_PATH%
tools\protoc.exe "proto_battle.proto" --proto_path=.\ --include_imports=%cd% --python_out=%SERVER_PATH%
tools\protoc.exe "proto_server.proto" --proto_path=.\ --include_imports=%cd% --python_out=%SERVER_PATH%
tools\protoc.exe "proto_avatar.proto" --proto_path=.\ --include_imports=%cd% --python_out=%SERVER_PATH%

python -B gen.py

) else  (
	echo server_py目录不存在 
)

echo .
echo 导出完成!
pause 