REM nuget spec
msbuild Voodoo.csproj /t:Build /p:Configuration="Release net40"
msbuild Voodoo.csproj /t:Build /p:Configuration="Release net45"
msbuild Voodoo.csproj /t:Build /p:Configuration="Release net451"
msbuild Voodoo.csproj /t:Build /p:Configuration="Release net452"
msbuild Voodoo.csproj /t:Build /p:Configuration="Release net46"

C:\Users\Shawn\.dnx\runtimes\dnx-clr-win-x86.1.0.0-beta4\bin\dnx.exe --appbase "Z:\Dropbox\Lib\Projects\voodoo\Voodoo" "C:\Users\Shawn\.dnx\runtimes\dnx-clr-win-x86.1.0.0-beta4\bin\lib\Microsoft.Framework.PackageManager\Microsoft.Framework.PackageManager.dll" pack "Z:\Dropbox\Lib\Projects\voodoo\Voodoo" --configuration Release --out "bin"

nuget pack voodoo.nuspec
