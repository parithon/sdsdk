param
(
  [switch]
  $Release
)
$Configuration = $(if ($Release.IsPresent) { "Release" } else { "Debug" })
Write-Output "Building in ${Configuration} mode."
dotnet build .\Parithon.StreamDeck.SDK.Core --configuration $Configuration
dotnet build .\Parithon.StreamDeck.SDK.MSBuild.Tool --configuration $Configuration
dotnet build .\Parithon.StreamDeck.SDK --configuration $Configuration
dotnet build .\Parithon.StreamDeck.SDK.MSBuild --configuration $Configuration
if ($Configuration -eq 'Debug') { 
  dotnet build .\Parithon.StreamDeck.SDK.TestConsole --configuration $Configuration
}
