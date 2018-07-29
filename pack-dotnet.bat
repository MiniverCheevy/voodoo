dotnet build .\Voodoo.Patterns\Voodoo.Patterns.csproj --configuration Release
dotnet pack .\Voodoo.Patterns\Voodoo.Patterns.csproj --include-symbols --configuration Release
cd .\Voodoo.Patterns\bin\Release
dir