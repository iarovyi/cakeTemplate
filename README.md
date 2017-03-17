# How to add continious integration from scratch for .NET project

## Add AppVeyor continious integration
1) set before build script
  `nuget restore .\src\SomeProject.sln`
2) set artifacts path
  `.\src\SomeProject\bin\**\*.exe`

## Add Cake buld

1) [Add cake](http://cakebuild.net/docs/tutorials/setting-up-a-new-project)
  `nvoke-WebRequest http://cakebuild.net/download/bootstrapper/windows -OutFile build.ps1`

2) [Create file build.cake](http://cakebuild.net/docs/tutorials/setting-up-a-new-project)

```
var target = Argument("target", "Default");
 
 Task("Default")
   .Does(() =>
 {
   Information("Hello World!");
 });
 
 RunTarget(target);
```
3) run `./build.ps1`
4) [Restore nuget packages](http://cakebuild.net/api/Cake.Common.Tools.NuGet/NuGetAliases/09815AAD)
```
Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore("./src/SomeProject.sln");
});
```

`.IsDependentOn("Restore-NuGet-Packages")`

5) [Build solution](http://cakebuild.net/dsl/msbuild/)
```
var configuration = Argument("configuration", "Release");
	
Task("Build")
  .Does(() =>
{
  MSBuild("./src/SomeProject.sln", settings => settings.SetConfiguration(configuration));
});
```

 `.IsDependentOn("Build")`

6) [Add xunit tests](http://cakebuild.net/dsl/xunit-v2/)
`#tool "nuget:?package=xunit.runner.console"`

```
Task("Run-Tests")
	.IsDependentOn("Build")
    .Does(() =>
{
	XUnit2("./src/**/bin/Release/*.Specs.dll");
});
```

`.IsDependentOn("Run-Tests")`

## Use cake script on AppVeyor
`.\build.ps1`
