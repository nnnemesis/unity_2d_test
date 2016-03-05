using UnityEngine;
using System.Collections;

// Horizontal State
public enum MoveState
{
    Right,
    Left,
    Idle
}

public enum VerticalMoveState
{
    Up,
    Down,
    Idle
}

public enum JumpState
{
    Jump,
    Fall,
    Grounded,
}

public enum AttackState
{
    Idle,
    MainAttack
}

public class ChangedShiftWalkEvent : TireEvent
{
    public ChangedShiftWalkEvent() { Type = TireEventType.ChangedShiftWalkEvent; }
    public bool NewState;

    public override string ToString()
    {
        return "Type " + Type + " NewState " + NewState;
    }
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

public class ChangedVerticalMoveStateEvent : TireEvent
{
    public ChangedVerticalMoveStateEvent() { Type = TireEventType.ChangedVerticalMoveStateEvent; }
    public VerticalMoveState NewState;

    public override string ToString()
    {
        return "Type " + Type + " MoveState " + NewState;
    }
}

public class ChangedMoveStateEvent : TireEvent
{
    public ChangedMoveStateEvent() { Type = TireEventType.ChangedMoveStateEvent; }
    public MoveState NewState;
    public bool ShiftWalk = false;

    public override string ToString()
    {
        return "Type " + Type + " MoveState " + NewState + " ShiftWalk " + ShiftWalk;
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

public class ChangedUnitMoveControlType : TireEvent
{
    public ChangedUnitMoveControlType() { Type = TireEventType.ChangedUnitMoveControlType; }
    public UnitMoveControlType NewState;

    public override string ToString()
    {
        return "Type " + Type + " NewState " + NewState;
    }
}

public class ChangedCanUseLadder : TireEvent
{
    public ChangedCanUseLadder() { Type = TireEventType.ChangedCanUseLadder; }
    public bool NewState;

    public override string ToString()
    {
        return "Type " + Type + " NewState " + NewState;
    }
}

public class ChangedCanPickupAmmoEvent : TireEvent
{
    public ChangedCanPickupAmmoEvent() { Type = TireEventType.ChangedCanPickupAmmoEvent; }
    public bool NewState;

    public override string ToString()
    {
        return "Type " + Type + " NewState " + NewState;
    }
}

public class ChangedCurrentWeapon : TireEvent
{
    public ChangedCurrentWeapon() { Type = TireEventType.ChangedCurrentWeapon; }
    public WeaponType NewWeaponType = WeaponType.None;
    public int NewWeaponIndex = -1;

    public override string ToString()
    {
        return "Type " + Type + " NewWeaponType " + NewWeaponType + " NewWeaponIndex " + NewWeaponIndex;
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
