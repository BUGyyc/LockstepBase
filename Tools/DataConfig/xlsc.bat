@set XLS_NAME=%1
@set SHEET_NAME=%2

@echo =========Compilation of %SHEET_NAME% =========


::---------------------------------------------------
::第一步，将xls经过xls_deploy_tool转成data和proto
::---------------------------------------------------
@set EXECUTE_PATH=executeFile
@set DATA_PATH=..\..\..\oasis-anchang\Assets\StreamingAssets\ConfigData
@set CS_PATH=..\..\..\oasis-anchang\Assets\Shared\DataConfigDef
@set EXCEL_PATH=..\DataConfig

@cd %EXECUTE_PATH%

@python xls_deploy_tool.py %SHEET_NAME% %EXCEL_PATH%\%XLS_NAME%.xls

::---------------------------------------------------
::第二步，生成CS文件，并放到Assets目录下
::---------------------------------------------------

@protoc.exe "%SHEET_NAME%.proto" --csharp_out=%CS_PATH% --experimental_allow_proto3_optional

::---------------------------------------------------
::第三步，将data文件拷贝到Assets目录下,并删除零时文件
::---------------------------------------------------

@copy>nul 2>nul *.data %DATA_PATH%\*.bytes


@rem @copy %XLS_NAME%_pb2.py ..\py_out\*_pb2.py
@rem @del *_pb2.py

@del *.proto
@del *.data
@del *.log
@del *.txt