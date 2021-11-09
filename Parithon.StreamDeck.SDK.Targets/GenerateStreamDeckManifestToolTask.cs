using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Parithon.StreamDeck.SDK.Targets
{
  public class GenerateStreamDeckManifestToolTask : ToolTask
  {
    [Required]
    public string Command { get; set; }

    [Required]
    public string TargetPath { get; set; }

    protected override string ToolName => "sdgenman";

    protected override MessageImportance StandardOutputLoggingImportance => MessageImportance.High;

    protected override string GenerateFullPathToTool()
    {
      // When building with MSBuild from Visual Studio, we need
      // to use the Full framework. Otherwise, we need
      // to use the Core framework.
      return Command.EndsWith(".dll") ? "dotnet" : Command;
    }

    protected override string GenerateCommandLineCommands()
    {
      return Command.EndsWith(".dll") ? $"{Command} {TargetPath}" : TargetPath;
    }
  }
}
