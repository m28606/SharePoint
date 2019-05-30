
Param (
	[Parameter(Mandatory=$true, HelpMessage="URL of site collection, to which solution belongs.")]
	[string] $SiteUrl
)


$currentLocation = Split-Path $script:MyInvocation.MyCommand.Path
if(-not $tpcipSharedLoaded)
{
	& $currentLocation\Shared.ps1
}



if (Get-SPSite -limit All | where {$_.Url -eq $SiteUrl})
{
	$WebAppUrl = (Get-SPSite $SiteUrl -limit 1).WebApplication.Url.TrimEnd('/');
}
else
{
	throw "WebAppUrl cannot be set, because site collection '$SiteUrl' doeasn't exist."
}		
Message -message "Webapp url  " -NoNewLine
Message -info $WebAppUrl


$list = Get-ChildItem $currentLocation | where {$_.extension -eq ".wsp"}
	& "$currentLocation\CreateLogsFolder.ps1"
	foreach($solutionName in Get-ChildItem $list)
	{
		$SolName=$solutionName.Name
		Message -info $SolName
		$slnToRemove = Get-SPSolution | where {$_.Name -eq $SolName}
		Message -info $slnToRemove
		if($slnToRemove){
			Message -section "Uninstall solution $SolName"
			$slnToRemove | Uninstall-FarmSolution
			$slnToRemove | Remove-SPSolution -Confirm:$false
		}
	}

$ErrorActionPreference = $prevErrorActionPreference
