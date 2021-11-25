using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Framework;
using MSBuildTask = Microsoft.Build.Utilities.Task;

namespace Parithon.StreamDeck.SDK.MSBuild
{
  public class GetReverseProjectName : MSBuildTask
  {
    [Required]
    public string ProjectName { get; set; }

    [Output]
    public string ProjectReverseName { get; set; }

    public override bool Execute()
    {
      string[] splits = ProjectName.Split('.');
      Array.Reverse(splits);
      ProjectReverseName = string.Join(".", splits);
      return true;
    }
  }
}
