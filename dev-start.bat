@echo off
echo Starting RecordManagement API...

REM Kill any existing instances
taskkill /F /IM "RecordManagement.Api.exe" 2>nul
taskkill /F /IM "dotnet.exe" 2>nul

REM Build and run the project
cd /d "%~dp0"
echo Building project...
dotnet build "src\RecordManagement.Api\RecordManagement.Api.csproj"

if %errorlevel% equ 0 (
    echo ✅ Build successful
    echo 🌐 Starting API on http://localhost:5094
    echo 📖 Swagger UI will be available at http://localhost:5094/swagger
    
    REM Open browser to Swagger UI after a short delay
    start "" cmd /c "timeout /t 3 /nobreak >nul && start http://localhost:5094/swagger"
    
    REM Run the project
    dotnet run --project "src\RecordManagement.Api"
) else (
    echo ❌ Build failed
    pause
)
