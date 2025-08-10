@echo off
echo Starting RecordManagement API...

REM Load configuration if available
set "API_PORT=5094"
set "BROWSER_DELAY=3"

if exist "dev-config.local.bat" (
    echo 📋 Loading local configuration...
    call "dev-config.local.bat"
) else if exist "..\angular-simple-test\dev-config.local.bat" (
    echo 📋 Loading configuration from frontend project...
    call "..\angular-simple-test\dev-config.local.bat"
) else if exist "..\angular-simple-test\dev-config.bat" (
    echo 📋 Loading default configuration from frontend project...
    call "..\angular-simple-test\dev-config.bat"
)

REM Kill any existing instances
taskkill /F /IM "RecordManagement.Api.exe" 2>nul
taskkill /F /IM "dotnet.exe" 2>nul

REM Build and run the project
cd /d "%~dp0"
echo Building project...
dotnet build "src\RecordManagement.Api\RecordManagement.Api.csproj"

if %errorlevel% equ 0 (
    echo ✅ Build successful
    echo 🌐 Starting API on http://localhost:%API_PORT%
    echo 📖 Swagger UI will be available at http://localhost:%API_PORT%/swagger
    
    REM Open browser to Swagger UI after a short delay
    start "" cmd /c "timeout /t %BROWSER_DELAY% /nobreak >nul && start http://localhost:%API_PORT%/swagger"
    
    REM Run the project
    dotnet run --project "src\RecordManagement.Api"
) else (
    echo ❌ Build failed
    pause
)
