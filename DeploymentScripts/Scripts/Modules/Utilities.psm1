
Add-PSSnapin microsoft.sharepoint.powershell -erroraction "silentlycontinue"


function Message(
	[string] $Message,		# Simple Write-Host
	[switch] $Header,		# === Green ===
	[switch] $Section,		# --- Green ---
	[switch] $Warning,		# Yellow on black, 'Warning:' prefix
	[switch] $Error,		# Red, 'Error' prefix
	[switch] $Info,			# Cyan
	[switch] $Success,		# Green
	[Parameter(HelpMessage = "Example usage: ##14This text is yellow color but## this is default color. Use numbers 01-14")]
	[switch] $Color,
	[switch] $NoNewline
)	 
{	
	$screenWidth = $host.UI.RawUI.WindowSize.Width;
	if(-not $screenWidth)
	{
		$screenWidth = 80;
	}

	if ($Header)
	{
		$lines = "=" * (ToZeroIfNegative {$screenWidth - $Message.Length - 5})
		Write-Host ("`r`n{0} {1} {2}`r`n" -f "==", $Message, $lines) -ForegroundColor Green
		return
	}
	if ($Section)
	{
		$lines = "-" * (ToZeroIfNegative { $screenWidth - $Message.Length - 6 } )
		Write-Host ("{0} {1} {2}" -f "---", $Message, $lines) -ForegroundColor Green
		return
	}
	$foreground = $Host.UI.RawUI.ForegroundColor
	$background = $Host.UI.RawUI.BackgroundColor
	
	if ($Info)
	{
		$Host.UI.RawUI.ForegroundColor = 'Cyan'		
	}
	elseif ($Warning)
	{
		if ($Host.UI.RawUI.BackgroundColor -eq 'White')
		{
			$Host.UI.RawUI.BackgroundColor = 'Black'
		}			
		$Host.UI.RawUI.ForegroundColor = 'Yellow'
		Write-Host 'Warning: ' -NoNewline
	}
	elseif ($Error)
	{
		Write-Error $Message
		return
	}
	elseif ($Success)
	{
		$Host.UI.RawUI.ForegroundColor = 'Green'
	}
	elseif ($Info)
	{
		$Host.UI.RawUI.ForegroundColor = 'Cyan'
	}
	elseif ($Color)
	{
	 	$colorToken = "##"
		$colorTokenLength = $colorToken.tostring().length
		$defaultcolor = "00"
	    $colorLength = "2"
	    $stringArr = $Message.split($colorToken, [StringSplitOptions]::RemoveEmptyEntries)
		$defaultHostColor = $Host.UI.RawUI.get_ForegroundColor()
		$i = 1
		foreach ($string in $stringArr) {
			if($i % 2 -eq 1){
			 	write-host -foregroundcolor  $defaultHostColor "$string" -nonewline
			}
			else
			{
				$foregroundColor = $string.substring(0, $colorLength)
		        if ($foregroundColor -gt 15 -or ($foregroundColor -notmatch "^0[0-9]$" -and $foregroundColor -notmatch "^1[0-5]$")) { 
					write-host -foregroundcolor  $defaultHostColor "$string" -nonewline
		        }
				else
				{
					$string = $string.substring($colorTokenLength, $string.length - 2)
		            write-host -foregroundcolor $foregroundColor "$string" -nonewline
				}
			}
			$i++
		}
		write-host
		return
	}
	if($NoNewline)
	{
		Write-Host -NoNewLine $Message
	}
	else
	{
		Write-Host $Message
	}
	
	$Host.UI.RawUI.ForegroundColor = $foreground 
	$Host.UI.RawUI.BackgroundColor = $background
}


# Applies like operator over the array
function ContainsLike
{
	Param(
		[String[]] $Wildcards,
		[string] $Tested		
	)
	
	foreach ($wildcard in $Wildcards)
	{
		if ($Tested -like $wildcard) { return $true }
	}
	return $false
}


# Trims negative expression result to zero
function ToZeroIfNegative
{
	param (
		[Scriptblock]
		$ExpressionBlock
	)
	
	$result = $ExpressionBlock.Invoke()
	if ($result -lt 0) { return 0 }
	return $result
}