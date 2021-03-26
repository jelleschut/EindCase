$cert = @(Get-ChildItem cert:\CurrentUser\My -CodeSigning)[0] 
Set-AuthenticodeSignature .\StartEindcase.ps1 $cert

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$serverDir = 
Start-Process "$scriptDir\Backend\Backend\EindCase.Api\bin\Release\net5.0\EindCase.Api.exe"

cd "$scriptDir\Frontend\frontend\"

npx lite-server
