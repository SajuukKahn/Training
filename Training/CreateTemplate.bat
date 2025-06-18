@echo off
setlocal

REM Check for parameter or prompt the user
if "%~1"=="" (
    set /p EXAMPLE_NAME=Enter example folder name: 
) else (
    set EXAMPLE_NAME=%~1
)

REM Define paths
set TEMPLATE_FILE=example_template.zip
set DEST_DIR=wwwroot\content\%EXAMPLE_NAME%

REM Check if template file exists
if not exist "%TEMPLATE_FILE%" (
    echo Error: Template file "%TEMPLATE_FILE%" not found.
    exit /b 1
)

REM Create destination directory
mkdir "%DEST_DIR%" >nul 2>&1

REM Extract the template into the destination using PowerShell
powershell -Command "Expand-Archive -Path '%TEMPLATE_FILE%' -DestinationPath '%DEST_DIR%' -Force"

if %ERRORLEVEL% NEQ 0 (
    echo Failed to extract archive.
    exit /b 1
)

echo Example '%EXAMPLE_NAME%' created at %DEST_DIR%
endlocal
