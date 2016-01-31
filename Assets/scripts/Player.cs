using UnityEngine;
using System.Collections;

public enum PlayerMoveState
{
    Walk,
    ShiftWalk,
    Idle
}

public enum PlayerJumpState
{
    Jump,
    Fall,
    Grounded
}

public enum PlayerAttackState
{
    Idle,
    MainAttack
}

public class PlayerChangedDirectionEvent : TireEvent
{
    public PlayerChangedDirectionEvent() { Type = TireEventType.PlayerChangedDirectionEvent; }
    public sbyte NewDirection;
}

public class PlayerChangedMoveStateEvent : TireEvent
{
    public PlayerChangedMoveStateEvent() { Type = TireEventType.PlayerChangedMoveStateEvent; }
    public PlayerMoveState NewState;
}

public class PlayerChangedJumpStateEvent : TireEvent
{
    public PlayerChangedJumpStateEvent() { Type = TireEventType.PlayerChangedJumpStateEvent; }
    public PlayerJumpState NewState;
}

public class PlayerChangedCurrentWeapon : TireEvent
{
    public PlayerChangedCurrentWeapon() { Type = TireEventType.PlayerChangedCurrentWeapon; }
    public IWeapon NewCurrentWeapon;
}
