using System;
using System.Reflection;

namespace Parithon.StreamDeck.SDK.Targets
{
  internal class Program
  {
    static void Main(string[] args)
    {
      try
      {
        Assembly buildAssembly = Assembly.LoadFrom(args[0]);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}
