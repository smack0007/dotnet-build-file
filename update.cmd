@ECHO OFF
PUSHD %~dp0
dotnet pack -c Release
dotnet tool update -g dotnet-build-file --add-source %~dp0bin\Release\
POPD
