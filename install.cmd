@ECHO OFF
PUSHD %~dp0
dotnet pack -c Release
dotnet tool install -g dotnet-build-file --add-source %~dp0bin\Release\
POPD