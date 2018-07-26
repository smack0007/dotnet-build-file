@ECHO OFF

REM Get all args except the first.
SET DOTNETBUILDFILEARGS=%*
CALL SET DOTNETBUILDFILEARGS=%%DOTNETBUILDFILEARGS:*%1=%%

dotnet msbuild %~dp0build-file.csproj /nologo "/p:File=%~f1" /t:restore
dotnet msbuild %~dp0build-file.csproj /nologo "/p:File=%~f1" %DOTNETBUILDFILEARGS%
RMDIR /s /q %~dp1obj
