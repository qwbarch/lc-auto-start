@echo off

call ./build.bat
powershell Compress-Archive^
    -Force^
    -Path "../bin/AutoStart.dll",^
          "../manifest.json",^
          "../icon.png",^
          "../README.md"^
    -DestinationPath "../bin/auto-start.zip"