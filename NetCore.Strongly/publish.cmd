@echo off
cd ../ClientConfig
dotnet bundle
dotnet run
cd ../NetCore.Strongly
dotnet pack -c release
set /p fname="Nuget name:"
nuget push bin/release/%fname% -source nuget.org
pause