@ECHO OFF

PUSHD %~dp0
dotnet build
.\bin\Debug\dotnet-build-file\netcoreapp2.1\dotnet-build-file.cmd %*
POPD