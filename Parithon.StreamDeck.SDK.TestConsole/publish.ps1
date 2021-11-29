. .\build.ps1 -Release
dotnet publish -c Release --runtime win-x86 --self-contained && dotnet publish -c Release --runtime osx-x64 --self-contained && dotnet build -t:StreamDeckDistribution -c Release