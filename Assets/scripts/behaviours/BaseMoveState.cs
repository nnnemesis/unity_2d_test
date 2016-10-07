using UnityEngine;
using System.Collections;

public class BaseMoveState : MonoBehaviour
{
    private IEventTire EventTire;
    public float WalkMoveForse = 5;
    public float JumpForse = 200;
    public float ShiftWalkMoveForse = 10;
    public Vector2 GroundCheckVector = Vector2.up * -0.1f;
    public Vector2 HorizontalMoveForce = Vector2.zero;
    public Vector2 VerticalMoveForce = Vector2.zero;

    private bool _CanUseLadder = false;
    public bool CanUseLadder
    {
        get { return _CanUseLadder; }
        set
        {
            if(_CanUseLadder != value)
            {
                _CanUseLadder = value;
                EventTire.SendEvent(TEPath.Up, TireEventType.ChangedCanUseLadder, value);
            }
        }
    }

    private UnitMoveControlType _MoveControlType = UnitMoveControlType.GroundControl;
    public UnitMoveControlType MoveControlType
    {
        get { return _MoveControlType; }
        set
        {
            if(_MoveControlType != value)
            {
                _MoveControlType = value;
                EventTire.SendEvent(TEPath.Up, TireEventType.ChangedUnitMoveControlType, value);
            }
        }
    }

    private JumpState _JumpState = JumpState.Grounded;
    public JumpState JumpState
    {
        get { return _JumpState; }
        set
        {
            if(_JumpState != value)
            {
                _JumpState = value;
                EventTire.SendEvent(TEPath.Up, TireEventType.ChangedJumpStateEvent, value);
            }
        }
    }

    private MoveState _MoveState = MoveState.Idle;
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
                EventTire.SendEvent(TEPath.Up, TireEventType.ChangedMoveStateEvent, new object[] {value, ShiftWalk});
            }
        }
    }

    public VerticalMoveState _VerticalMoveState = VerticalMoveState.Idle;
    public VerticalMoveState VerticalMoveState
    {
        get { return _VerticalMoveState; }
        set
        {
            if(_VerticalMoveState != value)
            {
                _VerticalMoveState = value;
                EventTire.SendEvent(TEPath.Up, TireEventType.ChangedVerticalMoveStateEvent, value);
            }
        }
    }

    private bool _ShiftWalk = false;
    public bool ShiftWalk
    {
        get { return _ShiftWalk; }
        set
        {
            if(value != _ShiftWalk)
            {
                _ShiftWalk = value;
                EventTire.SendEvent(TEPath.Up, TireEventType.ChangedShiftWalkEvent, value);
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
                EventTire.SendEvent(TEPath.Up, TireEventType.ChangedDirectionEvent, value);
            }                
        }

    }

    private bool _SitDown = false;
    public bool SitDown
    {
        get { return _SitDown; }
        set
        {
            if(_SitDown != value)
            {
                _SitDown = value;
                EventTire.SendEvent(TEPath.Up, TireEventType.ChangedSitDownEvent, value);
            }
        }
    }

    void Start()
    {
        EventTire = this.GetEventTire();
    }
    
}