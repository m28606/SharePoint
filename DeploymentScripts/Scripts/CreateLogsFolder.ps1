$folderName = "C:\TpCipLogs"

if (!(Test-Path -path $folderName))
{
	New-Item -type directory -path $folderName

	$objUser = New-Object System.Security.Principal.NTAccount("Everyone")
	$colRights = [System.Security.AccessControl.FileSystemRights]::Read
	$InheritanceFlag = [System.Security.AccessControl.InheritanceFlags]::ContainerInherit -bor [System.Security.AccessControl.InheritanceFlags]::ObjectInherit
	$PropagationFlag = [System.Security.AccessControl.PropagationFlags]::None
	$objType =[System.Security.AccessControl.AccessControlType]::Allow

	$ar = New-Object System.Security.AccessControl.FileSystemAccessRule ($objUser, $colRights, $InheritanceFlag, $PropagationFlag, $objType)

	$acl = Get-Acl $folderName
	$acl.SetAccessRule($ar)
	Set-Acl $folderName $acl
}