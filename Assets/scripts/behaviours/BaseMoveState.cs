using UnityEngine;
using System.Collections;

public class BaseMoveState : MonoBehaviour
{
    private IEventTire EventTire;
    public Rigidbody2D Rigidbody;
    public Transform Transform;
    public float CurrentMoveForse = 0;
    public float WalkMoveForse = 5;
    public float JumpForse = 200;
    public float ShiftWalkMoveForse = 10;
    public Vector2 GroundCheckVector = Vector2.up * -0.1f;

    private JumpState _JumpState = JumpState.Grounded;
    public JumpState JumpState
    {
        get { return _JumpState; }
        set
        {
            if(_JumpState != value)
            {
                _JumpState = value;
                EventTire.SendEvent(new ChangedJumpStateEvent() { NewState = value });
            }
        }
    }

    private MoveState _MoveState = MoveState.Idle;    // default is idle
    public MoveState MoveState
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
                EventTire.SendEvent(new ChangedMoveStateEvent() { NewState = value });
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
                EventTire.SendEvent(new ChangedDirectionEvent() { NewDirection = value });
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