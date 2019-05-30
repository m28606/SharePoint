
function  Deploy-FarmSolution
{
	[CmdletBinding()]
	param(
			[Parameter(Mandatory = $true, HelpMessage = "Web application url.")]
			[string] $WebAppUrl,
			[Parameter(Mandatory = $true, ValueFromPipeline=$True, ValueFromPipelinebyPropertyName=$False, HelpMessage = "Path to WSP.")]
			[string] $WspFullName,
			[string] $AdminServiceName = "SPAdminV4",
			[Parameter(HelpMessage = "True = if solution exists reinstall, False = skip")]
			[boolean] $upgradeSolutions = $true
		)
		
	begin
	{
		if ((Get-Service $adminServiceName).Status -eq 'Stopped')
	    {
	        Start-Service $adminServiceName
	    }
	}
	process
	{
		$solutionName = Split-Path -Path $WspFullName -Leaf
		$webApplication = Get-SPWebApplication -Identity $WebAppUrl
		$solution = Get-SPSolution | where {$_.name -eq $solutionName}
		
		if(-not $solution)
	    {
	     	Message ("Deploying farm solution '{0}' to web application '{1}'." -f $solutionName, $WebAppUrl)
			Add-FarmSolution -SolutionName $SolutionName -SolutionPath $WspFullName
			Install-FarmSolution -SolutionName $SolutionName -WebAppUrl $WebAppUrl
	    }
		else
		{
			Message ("Solution '{0}' is already on Farm." -f $solutionName)
		}
	}
	end
	{}
}


# helper function
function Add-FarmSolution
{
    param (
        [String]$SolutionPath,
        [String]$SolutionName
		)
    
	$void = Add-SPSolution -LiteralPath $solutionPath -Confirm:$false
	
	$solution = Get-SPSolution -Identity $solutionName
	
	if (-not $solution)
	{
		Throw ("Solution '{0}' was not added to farm." -f $SolutionName)
	}

	WaitForSolution $solution
	
	Message ("Solution '{0}' has been added to farm." -f $SolutionName)
}

# helper function
function Install-FarmSolution
{
    param (
        [String]$SolutionName, 
        [String]$webAppUrl
        )
	
	$solution = Get-SPSolution -Identity $SolutionName
	
	if ($solution.DeployedWebApplications.Contains($WebApplication))
	{
		Message "Solution '{0}' is already installed on web application." -f $solutionName
		return
	}

    if ($solution.ContainsWebApplicationResource)
    {
        Install-SPSolution -Identity $solutionName -GACDeployment -Force:$true -Confirm:$false -WebApplication $webAppUrl
    }
    else
    {
        Install-SPSolution -Identity $solutionName -GACDeployment -Force:$true -Confirm:$false
    }
	
    WaitForSolution -Solution $solution
	
    Message ("Solution '{0}' has been installed." -f $solutionName)
}


function Uninstall-FarmSolution {
    param (
		[Parameter(Mandatory=$True, ValueFromPipeline=$True, ValueFromPipelinebyPropertyName=$False)]
		[Microsoft.SharePoint.Administration.SPSolution] $Solution
    ) 

   	if($solution.Deployed)
    {
	    if($solution.DeployedWebApplications.Count -gt 0) 
        {
            Uninstall-SPSolution $Solution –AllWebApplications -Local -Confirm:$false
        }
        else
        {
            Uninstall-SPSolution $Solution -Local -Confirm:$false
        }
        do 
		{
            sleep 2
            $Solution = Get-SPSolution $Solution
        }
		while($Solution.JobExists -or $Solution.Deployed)
    }
	Message ("'{0}' deleted" -f $Solution.name )

}


# helper function
function WaitForSolution
{
    param (
        [Microsoft.SharePoint.Administration.SPSolution] $Solution
        )

    $lastStatus = ""
    while ($Solution.JobExists)
    {
        $currentStatus = $Solution.JobStatus
        if ($currentStatus -ne $lastStatus)
        {
                Write-Host "$currentStatus…" -nonewline
                $lastStatus = $currentStatus
        }
        Write-Host "." -nonewline
        sleep 1
    }
	Write-Host ""
}	


function  Enable-Feature
{
[CmdletBinding()]
param(
		[Parameter(Mandatory = $true, HelpMessage = "Name of feature.")]
		[string] $Feature,
		[Parameter(Mandatory = $true, HelpMessage = "Name of feature.")]
		[string] $SiteUrl
	)

	try
	{
		Enable-SPFeature -Identity $Feature -Url $SiteUrl -ErrorAction Stop
		Message ("Feature '{0}' was activated" -f $Feature)
	}
	catch [System.Management.Automation.ActionPreferenceStopException]
	{
		Message ("Feature '{0}' was not activated" -f $Feature)

		if((Get-SPFeature -Identity $Feature -ErrorAction SilentlyContinue) -ne $null)
		{
			Message ("Feature is already active" -f $Feature)	
		}	
	}
}


function  Disable-Feature
{
[CmdletBinding()]
param(
		[Parameter(Mandatory = $true, HelpMessage = "Name of feature.")]
		[string] $Feature,
		[Parameter(Mandatory = $true, HelpMessage = "Name of feature.")]
		[string] $SiteUrl
	)

	try
	{
		Disable-SPFeature -identity $Feature -url $SiteUrl -confirm:$false -ErrorAction Stop
		Message ("Feature '{0}' was deactivated" -f $Feature)
	}
	catch [System.Management.Automation.ActionPreferenceStopException]
	{
		Message ("Feature '{0}' was not deactivated" -f $Feature)
	}
}

function CreateCustomList
 {  
  [CmdletBinding()]
   Param (
	[Parameter(Mandatory = $false,HelpMessage="Site collection URL")]
	[string] $SiteUrl
    )
  try
	{
		$listName = "DepartmentList"  
		$spWeb = Get-SPWeb -Identity $SiteUrl  
		$listExist = $spWeb.Lists | where{$_.Title -eq $listName}
		if(-Not $listExist){
		$spTemplate = $spWeb.ListTemplates["Custom List"]  
		$spListCollection = $spWeb.Lists  
		$spListCollection.Add($listName, $listName, $spTemplate)  
		Write-Host "List has been created Successfully" -ForegroundColor Green
		$path = $spWeb.url.trim()  
		$spList = $spWeb.GetList("$path/Lists/$listName")  
	
		#Call the function to add single line of Text
		Add-SPSingleLineOfTextField $spList "RedirectTo" "RedirectTo" "TRUE" "FALSE"
		Add-SPSingleLineOfTextField $spList "PortalMode" "PortalMode" "TRUE" "FALSE"
		Add-SPSingleLineOfTextField $spList "Key" "Key" "TRUE" "TRUE"
	

	    #adding the field type(DepartmentList) to the list  
		Add-SPMultipleLinesOfTextField $spList "Departments" "Departments" "FALSE" 1000 "FALSE" "Compatible"
		Add-SPMultipleLinesOfTextField $spList "Mids" "Mids" "FALSE" 1000 "FALSE" "Compatible"

		#adding the field type(IsAll) to the list  
		$spFieldType = [Microsoft.SharePoint.SPFieldType]::Boolean  
		$fldAll=$spList.Fields.Add("IsAll", $spFieldType,$true)  
		$view=$spList.Views["All Items"]
		$view.ViewFields.Add($fldAll)
		$view.Update()
		#$spList.Update() 
		Write-Host "Added Column IsAll for departments" -ForegroundColor Green
	
		
	
	    $spList.Update()  
		Write-Host "Congratulations!!! All operations completed sucessfully" -ForegroundColor Green 
	}
	
	}
	catch [System.Management.Automation.ActionPreferenceStopException]
	{
		Message ("Error in List Creation" )	
	}
}

#Function to Add Multiple Lines of Text
Function Add-SPMultipleLinesOfTextField([Microsoft.SharePoint.SPList]$List, [string]$DisplayName, [string]$Name, [string]$Required, [string]$NumLines, [string]$RichText, [string]$RichTextMode)
{
    if(!$List.Fields.ContainsField($DisplayName))
    {
        #Frame Field XML
        $FieldXML = "<Field Type='Note' DisplayName='"+ $DisplayName +"' Name='"+ $Name +"' Required='"+ $Required +"' NumLines='"+ $NumLines +"' RichText='" + $RichText +"' RichTextMode='"+ $RichTextMode +"' Sortable='FALSE'/>"
        #Add Field
        $NewField=$List.Fields.AddFieldAsXml($FieldXML,$true,[Microsoft.SharePoint.SPAddFieldOptions]::AddFieldToDefaultView)
		
        write-host "New Multiple Linex of Text Field Added!" -ForegroundColor Green
    }
    else
    {
        write-host "Field exists already!" -f RED
    }        
}

#Function to Add a Single Line of Text
Function Add-SPSingleLineOfTextField([Microsoft.SharePoint.SPList]$List, [string]$DisplayName, [string]$Name, [string]$Required,[string]$UniqueValues )
{
    if(!$List.Fields.ContainsField($DisplayName))
    {
        $FieldXML = "<Field Type='Text' DisplayName='"+ $DisplayName +"' Required='"+ $Required +"' MaxLength='255' Name='"+ $Name +"' EnforceUniqueValues='"+ $UniqueValues +"' Indexed='TRUE'  />" 
        $NewField=$List.Fields.AddFieldAsXml($FieldXML,$true,[Microsoft.SharePoint.SPAddFieldOptions]::AddFieldToDefaultView)
        write-host "New Field Added!" -f Green
    }
    else
    {
        write-host "Field exists already!" -f RED
    }        
}