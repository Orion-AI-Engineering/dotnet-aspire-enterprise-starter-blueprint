$required = @('README.md','src','tests','infra','scripts')
foreach ($item in $required) {
    if (-not (Test-Path $item)) { throw "Missing $item" }
}
Write-Host "Repository structure looks valid."
