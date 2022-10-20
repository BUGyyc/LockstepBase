cd /d %~dp0

cd "LockstepServer"

rd /s /Q Assets
mklink /J Assets "%~dp0/LockstepClient/Assets"


rd /s /Q Packages
mklink /J Packages "%~dp0/LockstepClient/Packages"


rd /s /Q ProjectSettings
mklink /J ProjectSettings "%~dp0/LockstepClient/ProjectSettings"

echo "Successed..."

pause