@echo off
echo ============================================
echo  Preparing Installer Package
echo ============================================
echo.

REM Create Installer\App directory if it doesn't exist
if not exist "%~dp0Installer\App" (
    mkdir "%~dp0Installer\App"
    echo Created Installer\App directory.
)

REM Find the Release build (check multiple possible paths)
set SOURCE_DIR=
if exist "%~dp0FirstStepApp\bin\x86\Release\FirstStepApp.exe" (
    set SOURCE_DIR=%~dp0FirstStepApp\bin\x86\Release
    echo Found x86 Release build.
) else if exist "%~dp0FirstStepApp\bin\x64\Release\FirstStepApp.exe" (
    set SOURCE_DIR=%~dp0FirstStepApp\bin\x64\Release
    echo Found x64 Release build.
) else if exist "%~dp0FirstStepApp\bin\Release\FirstStepApp.exe" (
    set SOURCE_DIR=%~dp0FirstStepApp\bin\Release
    echo Found Any CPU Release build.
)

if "%SOURCE_DIR%"=="" (
    echo ERROR: Release build not found!
    echo Please build the solution in Release mode first.
    echo.
    echo In Visual Studio:
    echo   1. Select Release from the configuration dropdown
    echo   2. Build ^> Build Solution
    echo.
    pause
    exit /b 1
)

echo Source directory: %SOURCE_DIR%
echo.

REM Define the app directory
set APP_DIR=%~dp0Installer\App

REM Clear existing files in App directory
echo Clearing existing files...
del /Q "%APP_DIR%\*.*" 2>nul

REM Copy required files
echo Copying files...
copy "%SOURCE_DIR%\FirstStepApp.exe" "%APP_DIR%\" /Y
echo   Copied: FirstStepApp.exe

copy "%SOURCE_DIR%\FirstStepApp.exe.config" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\FirstStepApp.exe.config" echo   Copied: FirstStepApp.exe.config

copy "%SOURCE_DIR%\ClosedXML.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\ClosedXML.dll" echo   Copied: ClosedXML.dll

copy "%SOURCE_DIR%\ClosedXML.Parser.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\ClosedXML.Parser.dll" echo   Copied: ClosedXML.Parser.dll

copy "%SOURCE_DIR%\DocumentFormat.OpenXml.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\DocumentFormat.OpenXml.dll" echo   Copied: DocumentFormat.OpenXml.dll

copy "%SOURCE_DIR%\DocumentFormat.OpenXml.Framework.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\DocumentFormat.OpenXml.Framework.dll" echo   Copied: DocumentFormat.OpenXml.Framework.dll

copy "%SOURCE_DIR%\ExcelNumberFormat.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\ExcelNumberFormat.dll" echo   Copied: ExcelNumberFormat.dll

copy "%SOURCE_DIR%\Microsoft.Bcl.HashCode.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\Microsoft.Bcl.HashCode.dll" echo   Copied: Microsoft.Bcl.HashCode.dll

copy "%SOURCE_DIR%\RBush.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\RBush.dll" echo   Copied: RBush.dll

copy "%SOURCE_DIR%\SixLabors.Fonts.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\SixLabors.Fonts.dll" echo   Copied: SixLabors.Fonts.dll

copy "%SOURCE_DIR%\System.Buffers.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\System.Buffers.dll" echo   Copied: System.Buffers.dll

copy "%SOURCE_DIR%\System.IO.Packaging.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\System.IO.Packaging.dll" echo   Copied: System.IO.Packaging.dll

copy "%SOURCE_DIR%\System.Memory.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\System.Memory.dll" echo   Copied: System.Memory.dll

copy "%SOURCE_DIR%\System.Numerics.Vectors.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\System.Numerics.Vectors.dll" echo   Copied: System.Numerics.Vectors.dll

copy "%SOURCE_DIR%\System.Runtime.CompilerServices.Unsafe.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\System.Runtime.CompilerServices.Unsafe.dll" echo   Copied: System.Runtime.CompilerServices.Unsafe.dll

REM Copy Keyence SDK files
copy "%SOURCE_DIR%\Communication.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\Communication.dll" echo   Copied: Communication.dll

copy "%SOURCE_DIR%\Keyence.AutoID.SDK.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\Keyence.AutoID.SDK.dll" echo   Copied: Keyence.AutoID.SDK.dll

copy "%SOURCE_DIR%\VncClientControlCommon.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\VncClientControlCommon.dll" echo   Copied: VncClientControlCommon.dll

copy "%SOURCE_DIR%\VncClientControlCommonLib.dll" "%APP_DIR%\" /Y 2>nul
if exist "%APP_DIR%\VncClientControlCommonLib.dll" echo   Copied: VncClientControlCommonLib.dll

REM Copy icon file
if exist "%~dp0FirstStepApp\scanner.ico" (
    echo Copying custom icon...
    copy "%~dp0FirstStepApp\scanner.ico" "%APP_DIR%\" /Y
    echo   Icon copied: scanner.ico
)

echo.
echo ============================================
echo  Installer package prepared successfully!
echo ============================================
echo.
echo Files are in: %APP_DIR%
echo.
echo Next step: Copy the Installer folder to the target PC
echo and run Install_FirstStepApp.bat as Administrator.
echo.
pause
