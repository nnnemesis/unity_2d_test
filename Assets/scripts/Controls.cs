using UnityEngine;
using System.Collections.Generic;

public enum ControlAction
{
    WalkForward,
    WalkBack,
    Shift,
    Jump,
    MainAttack
}

public class ControlEvent : TireEvent
{
    public ControlEvent() { Type = TireEventType.ControlEvent; }
    public Dictionary<ControlAction, bool> Actions;

    public override string ToString()
    {
        return "Type "+ Type + " Actions " + Actions;
    }
}
