#tool "nuget:?package=xunit.runner.console"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
	
	
Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore("./src/SomeProject.sln");
});
	
Task("Build")
  .IsDependentOn("Restore-NuGet-Packages")
  .Does(() =>
{
  MSBuild("./src/SomeProject.sln", settings => settings.SetConfiguration(configuration));
});

Task("Run-Tests")
	.IsDependentOn("Build")
    .Does(() =>
{
	XUnit2("./src/**/bin/Release/*.Specs.dll");
});

Task("Default")
  .IsDependentOn("Run-Tests")
  .Does(() =>
{
  Information("Hello World!");
});

RunTarget(target);