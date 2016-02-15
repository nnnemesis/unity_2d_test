using UnityEngine;
using System.Collections;

public enum MoveState
{
    Walk,
    ShiftWalk,
    Idle
}

public enum JumpState
{
    Jump,
    Fall,
    Grounded
}

public enum AttackState
{
    Idle,
    MainAttack
}

public class ChangedDirectionEvent : TireEvent
{
    public ChangedDirectionEvent() { Type = TireEventType.ChangedDirectionEvent; }
    public sbyte NewDirection;

    public override string ToString()
    {
        return "Type " + Type + " Direction " + NewDirection;
    }
}

public class ChangedMoveStateEvent : TireEvent
{
    public ChangedMoveStateEvent() { Type = TireEventType.ChangedMoveStateEvent; }
    public MoveState NewState;

    public override string ToString()
    {
        return "Type " + Type + " MoveState " + NewState;
    }
}

public class ChangedJumpStateEvent : TireEvent
{
    public ChangedJumpStateEvent() { Type = TireEventType.ChangedJumpStateEvent; }
    public JumpState NewState;

    public override string ToString()
    {
        return "Type " + Type + " JumpState " + NewState;
    }

}

public class ChangedCurrentWeapon : TireEvent
{
    public ChangedCurrentWeapon() { Type = TireEventType.ChangedCurrentWeapon; }
    public IWeapon NewCurrentWeapon;

    public override string ToString()
    {
        return "Type " + Type + " CurrentWeapon " + NewCurrentWeapon;
    }

}

public class ChangedHealthEvent : TireEvent
{
    public ChangedHealthEvent() { Type = TireEventType.ChangedHealthEvent; }
    public float NewHealth;

    public override string ToString()
    {
        return "Type " + Type + " Health " + NewHealth;
    }

}
