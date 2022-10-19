@set EXECUTE_PATH=bat_out
@set DATA_PATH=..\..\oasis-anchang\Assets\StreamingAssets\ConfigData
@set CS_PATH=..\..\oasis-anchang\Assets\Shared\DataConfigDef

del  %DATA_PATH%\*.bytes
del  %CS_PATH%\*.cs
del  %EXECUTE_PATH%\*.bat

python exportAll.py

exportCodeDef.bat

pause