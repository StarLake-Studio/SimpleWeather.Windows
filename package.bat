@echo off
setlocal

echo ========================================
echo SimpleWeather Windows Packaging Script
echo ========================================

set CONFIGURATION=Release
set PLATFORM=x64
set OUTPUT_DIR=package
set PACKAGE_NAME=SimpleWeather-Windows

echo.
echo Building project...
dotnet publish -c %CONFIGURATION% -r win10-%PLATFORM% --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true

if %errorlevel% neq 0 (
    echo.
    echo Build failed!
    pause
    exit /b 1
)

echo.
echo Creating package directory...
if exist %OUTPUT_DIR% rmdir /s /q %OUTPUT_DIR%
mkdir %OUTPUT_DIR%

echo.
echo Copying files...
xcopy "bin\%PLATFORM%\%CONFIGURATION%\net6.0-windows10.0.19041.0\win10-%PLATFORM%\publish\" "%OUTPUT_DIR%\" /E /I /Y

echo.
echo Creating zip archive...
powershell Compress-Archive -Path "%OUTPUT_DIR%\*" -DestinationPath "%PACKAGE_NAME%-%PLATFORM%.zip" -Force

echo.
echo Cleaning up...
rmdir /s /q %OUTPUT_DIR%

echo.
echo Packaging completed successfully!
echo Package: %PACKAGE_NAME%-%PLATFORM%.zip
echo.

pause