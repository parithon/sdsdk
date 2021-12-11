param
(
    [switch]$Release
)
$Configuration = $(if ($Release.IsPresent) { "Release" } else { "Debug" })
if ($Release.IsPresent) {
    dotnet build -c $Configuration 
}