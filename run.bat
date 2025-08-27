@echo off
echo Running SimpleWeather for Windows...
cd /d "%~dp0"
dotnet run
if %errorlevel% == 0 (
    echo Application started successfully!
) else (
    echo Failed to start application!
)
pause