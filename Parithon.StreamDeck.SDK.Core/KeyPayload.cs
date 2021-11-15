using System;
using System.Collections.Generic;
using System.Text;

namespace Parithon.StreamDeck.SDK.Core
{
  public class KeyPayload
  {
    public dynamic Settings { get; private set; }
    public Coordinates Coordinates { get; private set; }
    public short State { get; private set; }
    public short UserDesiredState { get; private set; }
    public bool IsInMultiAction { get; private set; }
  }
}
