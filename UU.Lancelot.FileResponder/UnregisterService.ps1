param (
    [string]$ServiceName = "Lancelot FileResponder"  # Defaultní název služby
)

# Kontrola, zda služba existuje
$service = Get-Service -Name $ServiceName -ErrorAction SilentlyContinue
if (-not $service) {
    Write-Host "Služba '$ServiceName' neexistuje."
    Write-Host -NoNewLine 'Stiskněte klávesu pro ukončení...';
    $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
}

# Zastavení služby, pokud běží
if ($service.Status -eq 'Running') {
    Stop-Service -Name $ServiceName -Force
}

# Odstranění služby
sc.exe delete $ServiceName

Write-Host "Služba '$ServiceName' byla úspěšně odinstalována."

Write-Host -NoNewLine 'Stiskněte klávesu pro ukončení...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
