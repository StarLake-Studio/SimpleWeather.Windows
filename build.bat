@echo off
setlocal

echo ========================================
echo SimpleWeather Windows Build Script
echo ========================================

set CONFIGURATION=Release
set PLATFORM=x64

:menu
echo.
echo Select an option:
echo 1. Build Debug version
echo 2. Build Release version
echo 3. Build Release version (x86)
echo 4. Build Release version (x64)
echo 5. Clean build
echo 6. Exit
echo.

set /p choice=Enter your choice (1-6): 

if "%choice%"=="1" (
    set CONFIGURATION=Debug
    set PLATFORM=x64
    goto build
)

if "%choice%"=="2" (
    set CONFIGURATION=Release
    set PLATFORM=x64
    goto build
)

if "%choice%"=="3" (
    set CONFIGURATION=Release
    set PLATFORM=x86
    goto build
)

if "%choice%"=="4" (
    set CONFIGURATION=Release
    set PLATFORM=x64
    goto build
)

if "%choice%"=="5" (
    echo Cleaning build...
    if exist bin rmdir /s /q bin
    if exist obj rmdir /s /q obj
    if exist publish rmdir /s /q publish
    echo Clean completed.
    goto menu
)

if "%choice%"=="6" (
    exit /b
)

echo Invalid choice. Please try again.
goto menu

:build
echo.
echo Building SimpleWeather for Windows...
echo Configuration: %CONFIGURATION%
echo Platform: %PLATFORM%
echo.

dotnet build -c %CONFIGURATION% -r win10-%PLATFORM% --self-contained false

if %errorlevel% == 0 (
    echo.
    echo Build succeeded!
    echo Output location: bin\%PLATFORM%\%CONFIGURATION%\net6.0-windows10.0.19041.0\win10-%PLATFORM%\
) else (
    echo.
    echo Build failed!
)

echo.
pause
goto menu