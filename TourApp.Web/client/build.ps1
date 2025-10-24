$rootDir = Get-Location
$angularProjects = Get-ChildItem -Path $rootDir/projects -Exclude shared

try {
    ng build shared
    Write-Host "Build succeeded shared" -ForegroundColor Green
} catch {
    Write-Host "Build failed shared" -ForegroundColor Red
}

foreach ($project in $angularProjects) {
    Write-Host "Building Angular project: $project" -ForegroundColor Cyan

    Set-Location $project

    try {
        ng build
        Write-Host "Build succeeded $project" -ForegroundColor Green
    } catch {
        Write-Host "Build failed $project" -ForegroundColor Red
    }
}

Set-Location $rootDir
