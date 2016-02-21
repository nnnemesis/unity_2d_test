using UnityEngine;
using System.Collections.Generic;

public enum ControlAction
{
    WalkUp,
    WalkDown,
    WalkForward,
    WalkBack,
    Shift,
    Jump,
    MainAttack,
    Use,
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
