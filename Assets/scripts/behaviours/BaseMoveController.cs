using UnityEngine;
using System.Collections.Generic;

// controls unit movements on ground
public class BaseMoveController : MonoBehaviour, ITireEventListener {

    private BaseMoveState BaseState;
    private IEventTire EventTire;
    private Rigidbody2D Rigidbody;

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
        BaseState = GetComponent<BaseMoveState>();
        Rigidbody = GetComponent<Rigidbody2D>();

        Rigidbody.gravityScale = 1f;
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
        EventTire.AddEventListener(TireEventType.ChangedMoveStateEvent, this);
        EventTire.AddEventListener(TireEventType.ChangedShiftWalkEvent, this);
        EventTire.AddEventListener(TireEventType.ChangedJumpStateEvent, this);
    }

    void OnDestroy()
    {
        EventTire.RemoveEventListener(TireEventType.ControlEvent, this);
        EventTire.RemoveEventListener(TireEventType.ChangedMoveStateEvent, this);
        EventTire.RemoveEventListener(TireEventType.ChangedShiftWalkEvent, this);
        EventTire.RemoveEventListener(TireEventType.ChangedJumpStateEvent, this);
    }

    void FixedUpdate()
    {
        if(BaseState.JumpState != JumpState.Grounded)
        {
            float velocityY = Rigidbody.velocity.y;
            if (velocityY < 0)    // raycast only when fall!
            {
                if(velocityY < -5)
                {
                    BaseState.JumpState = JumpState.Fall;
                }                    
                Vector2 Position = transform.position;
                bool grounded = Physics2D.Linecast(Position, Position + BaseState.GroundCheckVector, LayerMask.GetMask("Ground"));
                if (grounded)
                {
                    BaseState.JumpState = JumpState.Grounded;
                }
            }
        }

        var horizontalMoveForce = BaseState.HorizontalMoveForce;
        if (horizontalMoveForce != Vector2.zero)
        {
            Rigidbody.AddForce(horizontalMoveForce);
        }
    }

    public void OnTireEvent(TireEvent ev)
    {
        if(ev.Type == TireEventType.ControlEvent)
        {
            OnControlEvent((ControlEvent)ev);
        }
        else if(ev.Type == TireEventType.ChangedMoveStateEvent)
        {
            UpdateHorizontalMoveState();
        }
        else if (ev.Type == TireEventType.ChangedShiftWalkEvent)
        {
            UpdateHorizontalMoveState();
        }
        else if (ev.Type == TireEventType.ChangedJumpStateEvent)
        {
            OnPlayerChangedJumpStateEvent((ChangedJumpStateEvent)ev);
        }
    }

    void UpdateHorizontalMoveState()
    {
        if (BaseState.MoveState == MoveState.Left)
        {
            if (BaseState.ShiftWalk)
            {
                BaseState.HorizontalMoveForce = BaseState.ShiftWalkMoveForse * Vector2.left;
            }
            else
            {
                BaseState.HorizontalMoveForce = BaseState.WalkMoveForse * Vector2.left;
            }
        }
        else if (BaseState.MoveState == MoveState.Right)
        {
            if (BaseState.ShiftWalk)
            {
                BaseState.HorizontalMoveForce = BaseState.ShiftWalkMoveForse * Vector2.right;
            }
            else
            {
                BaseState.HorizontalMoveForce = BaseState.WalkMoveForse * Vector2.right;
            }
        }
        else if (BaseState.MoveState == MoveState.Idle)
        {
            BaseState.HorizontalMoveForce = Vector2.zero;
        }
    }

    private void OnPlayerChangedJumpStateEvent(ChangedJumpStateEvent e)
    {
        if(BaseState.JumpState == JumpState.Jump)
        {
            Rigidbody.AddForce(Vector2.up * BaseState.JumpForse);
        }
    }

    private void OnControlEvent(ControlEvent e)
    {
        Dictionary<ControlAction, bool> actions = e.Actions;
        if(actions[ControlAction.WalkForward] || actions[ControlAction.WalkBack])
        {
            if (actions[ControlAction.Shift])
            {
                BaseState.ShiftWalk = true;
            }
            else
            {
                BaseState.ShiftWalk = false;
            }
        }
        else
        {
            BaseState.ShiftWalk = false;
        }

        if (actions[ControlAction.WalkForward])
        {
            BaseState.MoveState = MoveState.Right;
        }
        else if (actions[ControlAction.WalkBack])
        {
            BaseState.MoveState = MoveState.Left;
        }
        else
        {
            BaseState.MoveState = MoveState.Idle;
        }

        if (BaseState.JumpState == JumpState.Grounded)
        {
            if (actions[ControlAction.Jump])
            {
                BaseState.JumpState = JumpState.Jump;
            }
        }
    }
}
