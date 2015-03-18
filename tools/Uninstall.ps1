param($installPath, $toolsPath, $package, $project)

$file = Join-Path (Join-Path $toolsPath 'jsmodels') 'jsmodels.cmd.exe' | Get-ChildItem

$project.ProjectItems.Item($file.Name).Delete()