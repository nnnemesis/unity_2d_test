using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour
{
    private IEventTire EventTire;
    public Rigidbody2D Rigidbody;
    public Transform Transform;
    public float CurrentMoveForse = 0;
    public float WalkMoveForse = 5;
    public float ShiftWalkMoveForse = 10;
    public float JumpForse = 200;
    public Vector2 GroundCheckVector = Vector2.up * -0.1f;

    private PlayerJumpState _JumpState = PlayerJumpState.Grounded;
    public PlayerJumpState JumpState
    {
        get { return _JumpState; }
        set
        {
            if(_JumpState != value)
            {
                _JumpState = value;
                EventTire.SendEvent(new PlayerChangedJumpStateEvent() { NewState = value });
            }
        }
    }

    private PlayerMoveState _MoveState = PlayerMoveState.Idle;    // default is idle
    public PlayerMoveState MoveState
    {
        get
        {
            return _MoveState;
        }
        set
        {
            if(_MoveState != value)
            {
                _MoveState = value;
                EventTire.SendEvent(new PlayerChangedMoveStateEvent() { NewState = value });
            }
        }
    }

    private sbyte _Direction = 1; // default is right
    public sbyte Direction
    {
        get
        {
            return _Direction;
        }
        set
        {
            if (_Direction != value)
            {
                _Direction = value;
                EventTire.SendEvent(new PlayerChangedDirectionEvent() { NewDirection = value });
            }                
        }

    }

    private IWeapon _CurrentWeapon;
    public IWeapon CurrentWeapon
    {
        get { return _CurrentWeapon; }
        set
        {
            if (_CurrentWeapon != value)
            {
                _CurrentWeapon = value;
                EventTire.SendEvent(new PlayerChangedCurrentWeapon() { NewCurrentWeapon = value });
            }
        }
    }

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Transform = transform;
    }
    
}