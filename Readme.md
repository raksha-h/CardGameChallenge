# SAP Card Game Challenge

The game can be configured to have more than 2 player, currently it is hardcoded as 2 players in `Program.cs` file, if you wish to change player count please update the `PlayerCount` variable value in `Program.cs` 

Source code is written in C# using .NET Framework 4.6.1


## Software Requirement

Visual Studio 2019 Community Edition

## Building

Open `CardGameChallenge.sln` in Visual Studio 2019 and build solution

## Building in command line

```bat
Build.bat
```
Build.bat will  build visual studio solution in Debug and Release mode and if build is successful, then it will run unit tests on Debug and Release build.

Note: Build.bat uses `MSBuild.exe` from default location `%PROGRAMFILES(X86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe` If MSBuild.exe location is different in your machine, then please update the path in batch file.

## Output
Game will be built as windows console application and after build, release binary will be available at `Output\Release\SAP.ProgrammingChallenge.CardGame.exe`, debug binary will be available at `Output\Debug\SAP.ProgrammingChallenge.CardGame.exe`

## Third-party packages

For simplicity, Nunit (framework and console) is kept in External directory in repository