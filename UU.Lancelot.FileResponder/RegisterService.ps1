param (
    [string]$ServiceName = "Lancelot FileResponder",  # Defaultní název služby
    [string]$ExecutablePath = ".\bin\Debug\net8.0\win-x64\UU.Lancelot.FileResponder.exe"
)

# Kontrola, zda byla zadána cesta k .exe souboru
if (-not $ExecutablePath) {
    Write-Host "Prosím zadejte cestu k .exe souboru služby."
    Write-Host -NoNewLine 'Stiskněte klávesu pro ukončení...';
    $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
    exit 1
}
else
{
    # Kontrola, zda služba již existuje
    $service = Get-Service -Name $ServiceName -ErrorAction SilentlyContinue
    if ($service) {
        Write-Host "Služba '$ServiceName' již existuje."
        Write-Host -NoNewLine 'Stiskněte klávesu pro ukončení...';
    $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
        exit 2
    }
    # Instalace služby
    New-Service -Name $ServiceName -BinaryPathName $ExecutablePath -DisplayName $ServiceName -StartupType Automatic

    Write-Host "Služba '$ServiceName' byla úspěšně nainstalována."

    Write-Host -NoNewLine 'Stiskněte klávesu pro ukončení...';
    $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
}
