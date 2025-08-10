@echo off
echo Stopping RecordManagement API...

REM Kill any running instances
taskkill /F /IM "RecordManagement.Api.exe" 2>nul
taskkill /F /IM "dotnet.exe" 2>nul

echo API stopped.
