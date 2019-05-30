
Param (
	[Parameter(Mandatory=$true, HelpMessage="URL of site collection, to which should be added.")]
	[string] $SiteUrl
)


$currentLocation = Split-Path $script:MyInvocation.MyCommand.Path
if(-not $tpcipSharedLoaded)
{
	& $currentLocation\Shared.ps1
}



$featureName = "TPCIP.Web_FiddlerProxy"


Message -section "Enable feature'$featureName'"
Enable-Feature -Feature $featureName -SiteUrl $SiteUrl