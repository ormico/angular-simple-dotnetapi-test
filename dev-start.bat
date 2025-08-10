@echo off
echo Starting RecordManagement API...

REM Kill any existing instances
taskkill /F /IM "RecordManagement.Api.exe" 2>nul

REM Build and run the project
cd /d "%~dp0"
dotnet run --project "src\RecordManagement.Api"
