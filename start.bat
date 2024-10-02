@echo off
start /b docker run -p 8080:8080 task-backend
cd TaskTrackerFrontend\bin\Release\net8.0\win-x64\publish
start TaskTrackerFrontend.exe