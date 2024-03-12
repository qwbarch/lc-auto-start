@echo off

dotnet build ../src/AutoStart.csproj
rmdir /s /q "..\bin"
mkdir "..\bin"
move "..\src\bin\Debug\netstandard2.1\AutoStart.dll" "..\bin\AutoStart.dll"