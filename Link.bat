cd /d %~dp0

cd "LockstepServer"

rd /s /Q Assets
mklink /J Assets "%~dp0/Core/Assets"


rd /s /Q Packages
mklink /J Packages "%~dp0/Core/Packages"


rd /s /Q ProjectSettings
mklink /J ProjectSettings "%~dp0/Core/ProjectSettings"

echo "Successed..."

pause