del "*.nupkg"
"..\..\oqtane.framework\oqtane.package\nuget.exe" pack LLM.Module.LazyLoadingTest.nuspec 
XCOPY "*.nupkg" "..\..\oqtane.framework\Oqtane.Server\Packages\" /Y

