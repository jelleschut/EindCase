if($psISE) {
    $scriptDir = Split-Path -Path $psISE.CurrentFile.FullPath
}
else 
{
    $scriptDir = $Global:PSScriptRoot
}

cd $scriptDir

dotnet run -p ..\Backend\Backend\EindCase.Api\EindCase.Api.csproj
pause