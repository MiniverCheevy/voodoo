REM nuget spec
msbuild Voodoo.csproj /t:Build /p:Configuration="Release net40"
msbuild Voodoo.csproj /t:Build /p:Configuration="Release net45"
msbuild Voodoo.csproj /t:Build /p:Configuration="Release net451"
nuget pack Voodoo.csproj -Prop Configuration=Release
