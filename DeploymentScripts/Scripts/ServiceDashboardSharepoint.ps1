Param (
	[Parameter(HelpMessage="Site collection URL")]
	[string] $SiteUrl
)

#  Add-PSSnapin "Microsoft.SharePoint.PowerShell"
if ((Get-PSSnapin "Microsoft.SharePoint.PowerShell" -ErrorAction SilentlyContinue) -eq $null) 
{
	Add-PSSnapin "Microsoft.SharePoint.PowerShell"
}
#List of SharePoint Lists for ServiceDashboard
$global:arrServiceDashboardLists = "ServiceDetails","GuideDetails", "ToolDetails", "ServiceLogs"

#Error message enumeration 
Add-Type -TypeDefinition @"
   public enum MessageOptions
   {
      ERROR_MESSAGE,
      SUCCESS_MESSAGE,
      TITLE_MESSAGE,
	  INFO_MESSAGE,
	  WARNING_MESSAGE
   }
"@

#Get Current Script Path.
$ScriptPath = $MyInvocation.MyCommand.Path
$ScriptDir  = Split-Path -Parent $ScriptPath
$global:logFileFullPath = "$ScriptDir\errors.txt"
Function Prompt-Installation-Options (){
	Write-Message "`nSelect Operation to perform" TITLE_MESSAGE
	Write-Host "`t1. DEPLOY ALL LISTS   for Service Dashboard"
	Write-Host "`t2. DEPLOY SINGLE LIST for Service Dashboard"
	Write-Host "`t3. UNINSTALL Service Dashboard SharePoint Database"
	Write-Host "`t4. Press any other key to Exit"
	$Option = Read-Host -Prompt "> Enter Option "
	return $Option;
}
Function Log-Error([string] $Message){
	$dateTime = Get-Date 
	"`r`n`r`n$dateTime"| Out-File $global:logFileFullPath -Append 
	$Message |Out-File $global:logFileFullPath -Append
}
Function Write-Message([string] $Message, [MessageOptions]$MessageOption){
	
	switch ($MessageOption){
		"ERROR_MESSAGE" {
			Write-Host "#ERROR#`n`t$Message`n" -foregroundcolor "Red" -backgroundcolor "black";
			Log-Error "#ERROR#`n`t$Message`n";
			break;
		} 
		"SUCCESS_MESSAGE" {
			Write-Host "#SUCCESS#" $Message -foregroundcolor "Green" -backgroundcolor "black";
			break;
		} 
		"TITLE_MESSAGE" {
			Write-Host $Message -foregroundcolor "magenta"; 
			break; 
		} 
		"INFO_MESSAGE" {
			Write-Host "`t$Message" -foregroundcolor "DarkGray" -backgroundcolor "black";
			break; 
		}
		"WARNING_MESSAGE" {
			Write-Host "#WARNING#`n`t$Message`n" -foregroundcolor "Yellow" -backgroundcolor "black";
			break; 
		}		
		default {
			Write-Host "$Message";
			break;
		} 
	}
}
Function Change-Language ([System.Collections.ArrayList]$ServiceList, [string]$TemplateDir,[string]$SiteLanguage){
	$TemporaryDir=$TemplateDir+"\Temp"
	try{
		$List = @(get-childitem -path $ScriptDir  -include *.stp -Recurse -ErrorAction Stop) 
		for ($i=0; $i -lt $ServiceList.Count; $i++) {
			$CurrentListName=[string]$ServiceList[$i];
			$ListPath=$TemplateDir+"\"+$CurrentListName+".stp"
			$TempListSTPPath=$TemporaryDir+"\"+$CurrentListName+".stp"
			$TempCabFilePath=$TemporaryDir+"\"+$CurrentListName+".cab"
			$ExpandedFilePath=$TemporaryDir+"\"+$CurrentListName+"\"
			$ManifestDirectory=$TemporaryDir+"\"+$CurrentListName+"\"
			$ManifestPath=$TemporaryDir+"\"+$CurrentListName+"\manifest.xml"
			New-Item -ItemType Directory -Force -Path $TemporaryDir | out-null
			New-Item -ItemType Directory -Force -Path $ManifestDirectory | out-null
			Copy-Item  -Path $ListPath -Destination $TempListSTPPath -force
			rename-item $TempListSTPPath "$CurrentListName.cab"
			expand -r  $TempCabFilePath $ExpandedFilePath | out-null
			$LanguageTag="<Language>$SiteLanguage</Language>"
			(Get-Content $ManifestPath) | Foreach-Object {$_ -replace '(<Language>)\d{4}(</Language>)', $LanguageTag} | Set-Content $ManifestPath
			makecab $ManifestPath $TemporaryDir"\$CurrentListName"".stp" | out-null
		}
	}
	catch{
		Write-Message " Error Processing the *.stp Files `n`t$_.Exception.Message`t" ERROR_MESSAGE
		$TemporaryDir=$TemporaryDir+"\*"
		Remove-Item $TemporaryDir -Recurse 
	}
	#return $TemporaryDir;
}
Function Upload-List-Templates([string]$ScriptPath, [string]$SiteUrl, [string]$ListName="NOVA"){
	$ServiceList=New-Object System.Collections.ArrayList
	if ($ListName.Equals("NOVA")){
		$ServiceList.AddRange($global:arrServiceDashboardLists)
		Write-Message "Uploading Service Dashboard All Lists Template..." INFO_MESSAGE
	}
	else{
		$ServiceList.Add($ListName)
		Write-Message "Uploading Service Dashboard $ListName Template..." INFO_MESSAGE
	}
	$Flag=0
	try
	{
		$site = get-spsite("$SiteUrl") -ErrorAction Stop
		# Get the root web
		$web = $site.RootWeb
		$web.AllowUnsafeUpdates = $true
		# Get the list template gallery
		$spLTG = $web.GetFolder("_catalogs/lt")
		#Get templates files from the directory
		$Files = $spLTG.Files 
		#Get Current Script Directory
		$ScriptDir  = Split-Path -Parent $ScriptPath
		$ListTemplateDir= $ScriptDir+"\SharepointListTemplates"
		$SiteLanguage=$site.RootWeb.Language
		Change-Language $ServiceList $ListTemplateDir $SiteLanguage
		
		# PowerShell script to list the .stp files under Script Directory Folder
		try{
		$ListTemplateDir=$ListTemplateDir+"\Temp"
		$List = @(get-childitem -path $ListTemplateDir  -include *.stp -Recurse -ErrorAction Stop) 
		}
		catch{
		Write-Message "$SharepointListTemplates Cannot be created for processing;`n`t$_.Exception.Message `n" ERROR_MESSAGE
		}
		#Upload .stp files to the SharePoint Site.
		for ($i=0; $i -lt $List.Count; $i++) {
			$FileName = $List[$i].FullName.Substring($List[$i].FullName.LastIndexOf("\")+1) 
				if($ServiceList -contains $FileName.split("\.")[0])
				{
					try{
						$titleHashTbl = @{}
						$titleHashTbl.Add("Title", $FileName.split("\.")[0])
						$fileStream=$List[$i].OpenRead()
						$Files.Add("_catalogs/lt" +"/" + $FileName,$fileStream, $titleHashTbl,$false) |Out-Null
						Write-Message "$FileName UPLOADED SUCCESSFULLY `n" SUCCESS_MESSAGE 
						$fileStream.Close()
						$Flag=1
						}
					catch
						{
						Write-Message "`n`t$_.Exception.Message `n" ERROR_MESSAGE
						$Flag=1
						}
				}
		}
		#Dispose the objects
		$web.Dispose()
	}
	catch
	{
		Write-Message "`n`t$_.Exception.Message `n" ERROR_MESSAGE
		$Flag=1
	}
	Finally{
		$web.AllowUnsafeUpdates = $false 
		Remove-Item $ListTemplateDir -Recurse 
	}
	if($Flag -eq 0)
	{
		Write-Message "Please Check the List Templates are present on the script path" WARNING_MESSAGE
	}
}

Function Delete-Sharepoint-Lists([string]$SiteUrl, [string]$ListName="NOVA"){
	$ServiceList=New-Object System.Collections.ArrayList
	if ($ListName.Equals("NOVA"))
	{
		$ServiceList.AddRange($global:arrServiceDashboardLists)
		Write-Message "Deleting SharePoint all Lists..." INFO_MESSAGE
	}
	else
	{
		$ServiceList.Add($ListName)
		Write-Message "Deleting SharePoint List $ListName..." INFO_MESSAGE
	}
	$Flag=0
	try
	{
		$site = get-spsite("$SiteUrl") -ErrorAction Stop
		$web = $site.RootWeb
		$web.AllowUnsafeUpdates = $true 
		$lists = $web.Lists
		for ($i = 0; $i -lt $ServiceList.Count; $i++) {
			if($web.Lists[$ServiceList[$i]]){
				try{
					
						$web.Lists.Delete([System.Guid]$web.Lists[$ServiceList[$i]].ID)
						$Message = "List "+$ServiceList[$i]+" DELETED SUCCESSFULLY `n"
						Write-Message $Message SUCCESS_MESSAGE
						
					
				}
				catch{
					$Message= "List "+$ServiceList[$i]+" DOESN'T EXISTS;`n`t$_.Exception.Message`n"
					Write-Message $Message ERROR_MESSAGE
				}
				finally{
					$Flag=1
				}
			}
		}
		#Dispose the objects
		$web.Dispose()
	}
	catch
	{
		Write-Message "$_.Exception.Message" ERROR_MESSAGE
		$Flag=1
	}
	Finally{
		$web.AllowUnsafeUpdates = $false 
	}
	if($Flag -eq 0)
	{
	Write-Message "There are no Service Dashboard Lists on the Server to be deleted" WARNING_MESSAGE
	}
}

Function Delete-List-Templates([string]$SiteUrl, [string]$ListName="NOVA"){
	$ServiceList=New-Object System.Collections.ArrayList
	if ($ListName.Equals("NOVA"))
	{
		$ServiceList.AddRange($global:arrServiceDashboardLists)
		Write-Message "Deleting Service Dashboard All Lists Template..." INFO_MESSAGE
	}
	else
	{
		$ServiceList.Add($ListName)
		Write-Message "Deleting Service Dashboard $ListName Template..." INFO_MESSAGE
	}
	$Flag=0
	
	try
	{
		$site = get-spsite("$SiteUrl") -ErrorAction Stop
		$web = $site.RootWeb
		$web.AllowUnsafeUpdates = $true 
		$spfolder = $web.GetFolder("_catalogs/lt")
		$templateNames = New-Object System.Collections.Generic.List[System.Object]
		foreach ($file in $spfolder.Files) {
			$FileName=$file.Name
			if($ServiceList -contains $FileName.split("\.")[0]){
			$templateNames.Add($FileName)
			}
		}
		for ($i=0; $i -lt $templateNames.Count; $i++) {
			try
			{
				$FILE=$spfolder.Files[[string]$templateNames[$i]]
				if($FILE)
					{
					$FILE.Delete()
					$Message="Template "+$templateNames[$i]+" DELETED SUCCESSFULLY `n"
					Write-Message $Message SUCCESS_MESSAGE
					$Flag=1
					}
			}
			catch
			{
				$Message= "Template "+$templateNames[$i]+" NOT DELETED;`n`t$_.Exception.Message `n"
				Write-Message $Message ERROR_MESSAGE
				$Flag=1
			}
		}
		#Dispose the objects
		$web.Dispose()
	}
	catch
	{
		Write-Message "$_.Exception.Message" ERROR_MESSAGE
		$Flag=1
	}
	Finally{
		$web.AllowUnsafeUpdates = $false 
	}
	if($Flag -eq 0)
	{
	Write-Message "There are no Service Dashboard List Templates on the Server to be deleted" WARNING_MESSAGE
	}
		
}

Function Create-Lists-From-Templates([string]$SiteUrl, [string]$ListName="NOVA"){
	$ServiceList=New-Object System.Collections.ArrayList
	if ($ListName.Equals("NOVA"))
	{
		$ServiceList.AddRange($global:arrServiceDashboardLists)
		Write-Message "Creating Service Dashboard All Lists..." INFO_MESSAGE
	}
	else
	{
		$ServiceList.Add($ListName)
		Write-Message "Creating Service Dashboard $ListName List..." INFO_MESSAGE
	}
	$Flag=0
	try
	{
	$site = get-spsite("$SiteUrl") -ErrorAction Stop
	$web = $site.RootWeb
	$web.AllowUnsafeUpdates = $true 
	$CustomlistTemplates = $site.GetCustomListTemplates($web)
	foreach ($file in $CustomlistTemplates) {
		$FileName=$file.Name
		if($ServiceList -contains $FileName.split("\.")[0]){
			try{
				$web.Lists.Add("$FileName", "Custom list", $CustomlistTemplates["$FileName"])|out-null
				Write-Message "$FileName LIST CREATED SUCCESSFULLY `n" SUCCESS_MESSAGE
				$Flag=1
			}
			catch{
				Write-Message "$FileName LIST NOT CREATED;`n`t$_.Exception.Message `n" ERROR_MESSAGE
				$Flag=1
			}
		}
	}
	#Dispose the objects
	$web.Dispose()
	}
	catch
	{
		Write-Message "$_.Exception.Message" ERROR_MESSAGE
		$Flag=1
	}
	Finally{
		$web.AllowUnsafeUpdates = $false 
	}
	if($Flag -eq 0)
	{
	Write-Message "There are no Service Dashboard List Templates on the Server to create list from; `n`t Upload the list templates first" WARNING_MESSAGE
	}
}

Function Deploy-A-Sharepoint-List([string]$ScriptPath, [string]$SiteUrl){
	$ListCount=$global:arrServiceDashboardLists.Count
	Write-Message "`nSelect a list to deploy" TITLE_MESSAGE
	$index=0
	for ($i=0; $i -lt $ListCount; $i++) {
		$index=$i+1
		Write-Host "`t"$index"." $global:arrServiceDashboardLists[$i]
		}
		$index=$index+1
	Write-Host "`t"$index". Press any other key to Exit"
	$ListOption = Read-Host -Prompt "> Enter option "
	if($ListOption -le $ListCount -and $ListOption -gt 0)
	{
		$ListOption=$ListOption -1
		Delete-List-Templates $SiteUrl $global:arrServiceDashboardLists[$ListOption]
		Delete-Sharepoint-Lists $SiteUrl $global:arrServiceDashboardLists[$ListOption]
		Upload-List-Templates $ScriptPath $SiteUrl $global:arrServiceDashboardLists[$ListOption]
		Create-Lists-From-Templates $SiteUrl $global:arrServiceDashboardLists[$ListOption]
	}
}

Function Function-Main ([string]$ScriptPath,[string] $SiteUrl){
	$ExitOption=4
	$Option = Prompt-Installation-Options
	if($Option -lt $ExitOption)
	{
		Do{
			switch ($Option) 
			{ 
				1 {
					Delete-List-Templates $SiteUrl;
					Delete-Sharepoint-Lists $SiteUrl;
					Upload-List-Templates $ScriptPath $SiteUrl;
					Create-Lists-From-Templates $SiteUrl; 
					break
				}
				2 {
					Deploy-A-SharePoint-List $ScriptPath $SiteUrl;
					break
				}
				3 {
					Delete-List-Templates $SiteUrl;
					Delete-Sharepoint-Lists $SiteUrl;
					break
				}
				default {
				break;
				}
			}
			$Option = Prompt-Installation-Options
		}While($Option -lt $ExitOption -And $Option -gt 0)
	}
}

#Call Main Function 
Function-Main $ScriptPath $SiteUrl