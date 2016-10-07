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