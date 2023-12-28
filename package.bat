@echo off
call ./build.bat
powershell Compress-Archive -Force -Path "bin/Release/AutoStart.dll", "manifest.json", "icon.png", "README.md" -DestinationPath "package.zip"