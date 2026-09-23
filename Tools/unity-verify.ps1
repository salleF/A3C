param(
    [Parameter(Mandatory = $true)][string]$EditorPath,
    [switch]$SkipBuild
)
$ErrorActionPreference = 'Stop'
$project = Split-Path -Parent $PSScriptRoot
if (-not (Test-Path -LiteralPath $EditorPath -PathType Leaf)) { throw "Editor nao encontrado: $EditorPath" }
$logs = Join-Path $project 'Logs'
New-Item -ItemType Directory -Path $logs -Force | Out-Null
if (-not $SkipBuild) {
    $arguments = @('-batchmode', '-projectPath', ('"' + $project + '"'), '-executeMethod', 'A3C.Editor.A3CBuildTools.VerifyAndBuild', '-logFile', ('"' + (Join-Path $logs 'unity-build.log') + '"'))
    $process = Start-Process -FilePath $EditorPath -ArgumentList $arguments -WorkingDirectory $project -WindowStyle Hidden -PassThru
    if (-not $process.WaitForExit(1800000)) { throw "Unity ainda executa (PID $($process.Id)); consulte Logs/unity-build.log. Processo preservado." }
    if ($process.ExitCode -ne 0) { throw "Build falhou com codigo $($process.ExitCode). Consulte Logs/unity-build.log." }
}
$game = Join-Path $project 'Builds\WindowsRelease\A3C.exe'
$arguments = @('-batchmode', '-nographics', '-a3c-verify', '-a3c-report', ('"' + (Join-Path $logs 'a3c-runtime-verification.json') + '"'), '-logFile', ('"' + (Join-Path $logs 'runtime-verification.log') + '"'))
$process = Start-Process -FilePath $game -ArgumentList $arguments -WorkingDirectory $project -WindowStyle Hidden -PassThru
if (-not $process.WaitForExit(120000)) { throw "Verificacao excedeu 120 s (PID $($process.Id)); processo preservado para diagnostico." }
if ($process.ExitCode -ne 0) { throw "Runtime falhou com codigo $($process.ExitCode). Consulte Logs/runtime-verification.log." }
$result = Get-Content -Raw (Join-Path $logs 'a3c-runtime-verification.json') | ConvertFrom-Json
if (-not $result.passed) { throw $result.error }
$result | ConvertTo-Json
