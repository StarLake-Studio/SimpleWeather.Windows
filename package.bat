@echo off
setlocal

echo ========================================
echo SimpleWeather Windows Packaging Script
echo ========================================

set CONFIGURATION=Release
set PLATFORM=win10-x64
set OUTPUT_DIR=package
set PUBLISH_DIR=publish
set PACKAGE_NAME=SimpleWeather-Windows

echo.
echo Installing required workloads...
dotnet workload install windowsappsdk
dotnet workload install wasm-tools

echo.
echo Restoring dependencies...
dotnet restore

echo.
echo Building project...
dotnet build -c %CONFIGURATION%

echo.
echo Publishing project...
dotnet publish SimpleWeather.Windows.csproj -c %CONFIGURATION% -r %PLATFORM% --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true --output %PUBLISH_DIR%

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
xcopy "%PUBLISH_DIR%\*" "%OUTPUT_DIR%\" /E /I /Y

echo.
echo Creating zip archive...
powershell Compress-Archive -Path "%OUTPUT_DIR%\*" -DestinationPath "%PACKAGE_NAME%-%PLATFORM%.zip" -Force

echo.
echo Cleaning up...
rmdir /s /q %OUTPUT_DIR%
rmdir /s /q %PUBLISH_DIR%

echo.
echo Packaging completed successfully!
echo Package: %PACKAGE_NAME%-%PLATFORM%.zip
echo.

pause