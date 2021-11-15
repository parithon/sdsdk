using System;
using Parithon.StreamDeck.SDK;

namespace TestConsole
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var client = new StreamDeckClientBuilder(args)
        .Build();
    }
  }
}
