REM nuget spec
msbuild ./net40/Voodoo.Patterns.net40.csproj /t:Build /p:Configuration="Release"
msbuild ./net45/Voodoo.Patterns.net45.csproj /t:Build /p:Configuration="Release"
msbuild ./net46/Voodoo.Patterns.net46.csproj /t:Build /p:Configuration="Release"
msbuild ./net451/Voodoo.Patterns.net451.csproj /t:Build /p:Configuration="Release"
msbuild ./net452/Voodoo.Patterns.net452.csproj /t:Build /p:Configuration="Release"
msbuild ./netCoreApp10/Voodoo.Patterns.netCoreApp10.csproj /t:Build /p:Configuration="Release"
msbuild ./netStandard16/Voodoo.Patterns.netStandard16.csproj /t:Build /p:Configuration="Release"
msbuild ./profile78/Voodoo.Patterns.profile78.csproj /t:Build /p:Configuration="Release"
msbuild ./profile259/Voodoo.Patterns.profile259.csproj /t:Build /p:Configuration="Release"

nuget pack  voodoo.patterns.nuspec

msbuild ./net40/Voodoo.Patterns.net40.csproj /t:Build /p:Configuration="Debug"
msbuild ./net45/Voodoo.Patterns.net45.csproj /t:Build /p:Configuration="Debug"
msbuild ./net46/Voodoo.Patterns.net46.csproj /t:Build /p:Configuration="Debug"
msbuild ./net451/Voodoo.Patterns.net451.csproj /t:Build /p:Configuration="Debug"
msbuild ./net452/Voodoo.Patterns.net452.csproj /t:Build /p:Configuration="Debug"
msbuild ./netCoreApp10/Voodoo.Patterns.netCoreApp10.csproj /t:Build /p:Configuration="Debug"
msbuild ./netStandard16/Voodoo.Patterns.netStandard16.csproj /t:Build /p:Configuration="Debug"
msbuild ./profile78/Voodoo.Patterns.profile78.csproj /t:Build /p:Configuration="Debug"
msbuild ./profile259/Voodoo.Patterns.profile259.csproj /t:Build /p:Configuration="Debug"


nuget pack  voodoo.patterns.symbols.nuspec -Symbols
 