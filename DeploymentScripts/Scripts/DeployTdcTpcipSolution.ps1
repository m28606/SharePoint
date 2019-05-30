
Param (
	[Parameter(HelpMessage="Site collection URL")]
	[string] $SiteUrl
)

$currentLocation = Split-Path $script:MyInvocation.MyCommand.Path
$logFileFullPath = "$currentLocation\errors.txt"


$featureNameMasterpage = "TPCIP.WebStyling_TPCIP_MasterPage"
$featureNameHttpmodules = "TPCIP.Web_HttpModules"
$featureNameLogger = "TPCIP.Web_Logger"
$featureNameCustomErrors = "TPCIP.Web_CustomErrors"
$featureNameFiddler = "TPCIP.Web_FiddlerProxy"
$featureNameRedirectToCustomErrorPage = "TPCIP.Web_RedirectToCustomErrorPage"
$featureNameEmailConfigurationJob = "TPCIP.Web_EmailConfigurationJobFeature"
$featureNameDeletetingDataJob = "TPCIP.Web_DeletetingDataJobFeature"
$featureNameCreateList = "TPCIP.Web_LinksDetailsListCreateFeature"
$featureNameAutomaticNote = "TPCIP.Web_AutomaticNote"
$featureNameTimelineTemplateList = "TPCIP.ToolTimelineTemplate_CreateTimelineTemplateList"
$featureNameCustomerOverviewAdminList = "TPCIP.Web_CustomerOverviewAdminList"

Try 
{
	
	$list = Get-ChildItem $currentLocation | where {$_.extension -eq ".wsp"}
	& "$currentLocation\CreateLogsFolder.ps1"

	foreach($my_file in Get-ChildItem $list)
	{
		$Name=$my_file.Name
		if($Name -eq "TPCIP.Web.wsp")
		{
			#echo $Name
			& "$currentLocation\DeploySolution.ps1" -SiteUrl $SiteUrl -WspFullName "$currentLocation\$Name"
		}
	}

	foreach($my_file in Get-ChildItem $list)
	{
		$Name=$my_file.Name
		if($Name -ne "TPCIP.Web.wsp")
		{
			#echo $Name
			& "$currentLocation\DeploySolution.ps1" -SiteUrl $SiteUrl -WspFullName "$currentLocation\$Name"
		}
	}

	Message -section "Add DepartmentList if not added"
	CreateCustomList -SiteUrl $SiteUrl

	Message -section "Enable features"
	Enable-Feature -Feature $featureNameMasterpage -SiteUrl $SiteUrl
	Enable-Feature -Feature $featureNameHttpmodules -SiteUrl $SiteUrl
	Enable-Feature -Feature $featureNameLogger -SiteUrl $SiteUrl
	Enable-Feature -Feature $featureNameRedirectToCustomErrorPage -SiteUrl $SiteUrl

	Enable-Feature -Feature $featureNameDeletetingDataJob -SiteUrl $SiteUrl
	
	Enable-Feature -Feature $featureNameCreateList -SiteUrl $SiteUrl

	Enable-Feature -Feature $featureNameAutomaticNote -SiteUrl $SiteUrl

	Enable-Feature -Feature $featureNameTimelineTemplateList -SiteUrl $SiteUrl
		
	Enable-Feature -Feature $featureNameCustomerOverviewAdminList -SiteUrl $SiteUrl

	Disable-Feature -Feature $featureNameCustomErrors -SiteUrl $SiteUrl
	Disable-Feature -Feature $featureNameFiddler -SiteUrl $SiteUrl
	restart-service -displayname "SharePoint 2010 Timer"
}
Catch 
{
	$dateTime = Get-Date
	"`r`n`r`n$dateTime"| Out-File $logFileFullPath -Append
	$_ | Out-File $logFileFullPath -Append
}




