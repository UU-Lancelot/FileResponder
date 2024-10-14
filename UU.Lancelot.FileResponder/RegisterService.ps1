param (
    [string]$ServiceName = "Lancelot FileResponder",  # Defaultní název služby
    [string]$ExecutablePath = "C:\Users\marek\source\FileResponder\UU.Lancelot.FileResponder\bin\Debug\net8.0\win-x64\UU.Lancelot.FileResponder.exevy"                          # Cesta k .exe souboru
)

# Kontrola, zda byla zadána cesta k .exe souboru
if (-not $ExecutablePath) {
    Write-Host "Prosím zadejte cestu k .exe souboru služby."
    exit 1
}
else
{
    # Kontrola, zda služba již existuje
    $service = Get-Service -Name $ServiceName -ErrorAction SilentlyContinue
    if ($service) {
        Write-Host "Služba '$ServiceName' již existuje."
        exit 1
    }
    else
    {
        # Instalace služby
        New-Service -Name $ServiceName -BinaryPathName $ExecutablePath -DisplayName $ServiceName -StartupType Automatic

        Write-Host "Služba '$ServiceName' byla úspěšně nainstalována."

        # Pozastavení pro přečtení chybových hlášek
        Read-Host "Stiskněte Enter pro ukončení..."
    }
}
