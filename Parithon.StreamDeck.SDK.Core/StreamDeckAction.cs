using System;
using System.Collections.Generic;
using System.Text;

namespace Parithon.StreamDeck.SDK.Core
{
  public abstract class StreamDeckAction
  {
    public abstract string Icon { get; }

    public abstract string Name { get; }

    public virtual string PropertyInspectorPath { get; }

    public virtual ICollection<StreamDeckActionState> States { get; } = new List<StreamDeckActionState>();

    public virtual bool? SupportedInMultiActions { get; }

    public virtual string Tooltip { get; }

    public string UUID => GetType().FullName;

    public virtual bool? VisibleInActionsList { get; }

    public abstract void OnKeyDown(KeyPayload payload);

    public abstract void OnKeyUp(KeyPayload payload);
  }
}
