if($psISE) {
    $scriptDir = Split-Path -Path $psISE.CurrentFile.FullPath
}
else 
{
    $scriptDir = $Global:PSScriptRoot
}

npm install -g @angular/cli

cd $scriptDir
cd ..\Frontend\frontend

ng serve

pause