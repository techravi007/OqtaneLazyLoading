del "*.nupkg"
"..\..\oqtane520\oqtane.package\nuget.exe" pack LLM.Module.LazyLoadingTest.nuspec 
XCOPY "*.nupkg" "..\..\oqtane520\Oqtane.Server\Packages\" /Y

