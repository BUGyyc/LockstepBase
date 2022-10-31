cd /d %~dp0
echo "Start..."

cd "LockstepServer"


SET assets=.\Assets
SET scene=.\Assets\Scenes




IF NOT EXIST %assets%	(
	MD %assets%
)

IF NOT EXIST %scene%	(
	MD %scene%
)


cd "%~dp0/LockstepServer/Assets"

rd /s /Q ServerCore
mklink /J ServerCore "%~dp0/LockstepClient/Assets/LockstepCore/ServerCore"

	
rd /s /Q Common
mklink /J Common "%~dp0/LockstepClient/Assets/LockstepCore/Common"


cd "%~dp0/LockstepServer/Assets/Scenes"


rd /s /Q Server
mklink /J Server "%~dp0/LockstepClient/Assets/Scenes/Server"



echo "Successed..."

pause