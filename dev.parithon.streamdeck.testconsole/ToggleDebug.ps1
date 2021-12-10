if (-not $IsWindows) 
{
    Write-Warning "Non-windows environment still in development."
    return
}

$Path = "HKCU:\Software\Elgato Systems GmbH\StreamDeck"
$Name = "html_remote_debugging_enabled"
if (Get-ItemProperty -Path $Path -Name $Name -ErrorAction Ignore)
{
  Write-Output "Removing StreamDeck DEBUG setting";
  Remove-ItemProperty -Path $Path -Name $Name
}
else
{
  Write-Output "Adding StreamDeck DEBUG setting"
  New-ItemProperty -Path $Path -Name $Name -Value 1 -PropertyType DWord | Out-Null
}