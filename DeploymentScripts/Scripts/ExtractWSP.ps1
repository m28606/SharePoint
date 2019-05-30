# Load IIS module:
Import-Module ServerManager
Add-WindowsFeature Web-Scripting-Tools
Import-Module WebAdministration

$curDir=split-path -parent $MyInvocation.MyCommand.Definition
$rootfolder="C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\14\template"

$logFileFullPath = "$curDir\errors.txt"
$exportdir = "$curDir\" 
$site="Home Portal"

try
{
 #Retrieve the wsp files in this folder and subfolders 
 $s = [system.IO.SearchOption]::AllDirectories 
 $fileEntries = [IO.Directory]::GetFiles($curDir,"*.wsp",$s);  
 foreach($fullFileName in $fileEntries)  
 {  
    $fileName = $(Get-Item $fullFileName).Name; 
    $dirName =  $fileName.Replace(".wsp",""); 
    $path = $exportdir + $dirName; 
    $dir = [IO.Directory]::CreateDirectory($path)  
 
    #=========uncab WSP================
    Write-Host "Export $fileName started" -ForegroundColor Yellow  
    $destination = $path  
    C:\Windows\System32\extrac32.exe $fullFileName /e /Y /L $destination | out-null  
    Write-Host "Export $fileName completed" -ForegroundColor Yellow 
    
    #========copy ASCX Files===========
    $toolSrcDir= "$path\CONTROLTEMPLATES\*"
    Write-Host $toolSrcDir
    $toolDesDir="$rootfolder\CONTROLTEMPLATES"
    Write-Host $toolDesDir
    copy-item $toolSrcDir $toolDesDir -recurse -force

    #========copy JS Files===========
    $toolSrcDir= "$path\LAYOUTS\*"
    Write-Host $toolSrcDir
    $toolDesDir="$rootfolder\LAYOUTS"
    Write-Host $toolDesDir
    copy-item $toolSrcDir $toolDesDir -recurse -force	
  
    $Assembly="$path\$dirName.dll"
    
    #Set-Alias Name:Gacutil Value:
    
    & "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\x64\gacutil.exe" /i $Assembly

    $pool = (Get-Item "IIS:\Sites\$site"| Select-Object applicationPool).applicationPool
    Write-Host $pool
    Restart-WebAppPool $pool
 }
 Read-Host
}
catch
{
   $dateTime = Get-Date
   "`r`n`r`n$dateTime"| Out-File $logFileFullPath -Append
   $_ | Out-File $logFileFullPath -Append
}