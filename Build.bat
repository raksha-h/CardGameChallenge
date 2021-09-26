@echo off

set projectFile="%~dp0CardGameChallenge.proj"

rem -----------------------------
rem Getting MS Build exe location
rem -----------------------------

set MSBUILD=
if exist "%PROGRAMFILES(X86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" ( 
	set MSBUILD="%PROGRAMFILES(X86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" 
) else (
	echo ======================================================================================
	echo VS2019 MSBuild.exe is not found in given path, this build needs VS2019 to be installed
	echo If VS2019 is installed, please update the path of MSBuild.exe in this batch file
	echo ======================================================================================
	goto errorExit
)

echo ======================================================================================
echo Building solution using Visual Studio 2019
echo ======================================================================================
%MSBUILD% %projectFile% /nologo /m:1 /v:m /p:StopOnFirstFailure=true
if %ERRORLEVEL% NEQ 0 ( goto errorExit )

echo ======================================================================================
echo Running Unit tests on Debug build
echo ======================================================================================
"External\nunit-console\nunit3-console.exe" "Output\Debug\SAP.ProgrammingChallenge.CardGameTest.dll"
if %ERRORLEVEL% NEQ 0 ( goto errorExit )

echo ======================================================================================
echo Running Unit tests on Release build
echo ======================================================================================
"External\nunit-console\nunit3-console.exe" "Output\Release\SAP.ProgrammingChallenge.CardGameTest.dll"
if %ERRORLEVEL% NEQ 0 ( goto errorExit )

echo ======================================================================================
echo BUILD and TEST - COMPLETED SUCCESSFULLY
echo ======================================================================================

:normalExit
exit /b 0

:errorExit
exit /b 1