	

# Basic settings

$prevErrorActionPreference = $ErrorActionPreference
$ErrorActionPreference = 'Stop'



$modules =  @(
	"$currentLocation\Modules\Utilities",
	"$currentLocation\Modules\DeploymentFunctions.psm1"
)
Import-Module $modules -DisableNameChecking



$tpcipSharedLoaded = $true