using UnityEngine;
using System.Collections.Generic;

// controls unit movements on ground
public class BaseMoveController : MonoBehaviour {

    private BaseMoveState BaseState;
    private IEventTire EventTire;
    private Rigidbody2D Rigidbody;

    void Start()
    {
        EventTire = this.GetEventTire();
        BaseState = GetComponent<BaseMoveState>();
        Rigidbody = GetComponent<Rigidbody2D>();

        Rigidbody.gravityScale = 1f;
        EventTire.AddEventListener(TireEventType.ControlEvent, OnControlEvent);
        EventTire.AddEventListener(TireEventType.ChangedMoveStateEvent, UpdateHorizontalMoveState);
        EventTire.AddEventListener(TireEventType.ChangedShiftWalkEvent, UpdateHorizontalMoveState);
        EventTire.AddEventListener(TireEventType.ChangedJumpStateEvent, OnPlayerChangedJumpStateEvent);
    }

    void OnDestroy()
    {
        EventTire.RemoveEventListener(TireEventType.ControlEvent, OnControlEvent);
        EventTire.RemoveEventListener(TireEventType.ChangedMoveStateEvent, UpdateHorizontalMoveState);
        EventTire.RemoveEventListener(TireEventType.ChangedShiftWalkEvent, UpdateHorizontalMoveState);
        EventTire.RemoveEventListener(TireEventType.ChangedJumpStateEvent, OnPlayerChangedJumpStateEvent);
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

    void UpdateHorizontalMoveState(object param)
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

    private void OnPlayerChangedJumpStateEvent(object param)
    {
        if(BaseState.JumpState == JumpState.Jump)
        {
            Rigidbody.AddForce(Vector2.up * BaseState.JumpForse);
        }
    }

    private void OnControlEvent(object param)
    {
        Dictionary<ControlAction, bool> actions = (Dictionary<ControlAction, bool>)param;
        if(!actions[ControlAction.SitDown] && (actions[ControlAction.WalkForward] || actions[ControlAction.WalkBack]))
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
