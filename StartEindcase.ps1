$TLS12Protocol = [System.Net.SecurityProtocolType] 'Ssl3 , Tls12'
[System.Net.ServicePointManager]::SecurityProtocol = $TLS12Protocol

if($psISE) {
    $scriptDir = Split-Path -Path $psISE.CurrentFile.FullPath
}
else 
{
    $scriptDir = $Global:PSScriptRoot
}

start-process powershell $scriptDir\scripts\StartBackend.ps1
start-process powershell $scriptDir\scripts\StartFrontend.ps1