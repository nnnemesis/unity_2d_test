using UnityEngine;
using System.Collections.Generic;

// controls unit movements when unit on ladder
public class LadderMoveController : MonoBehaviour {

    private IEventTire EventTire;
    private Rigidbody2D Rigidbody;
    private BaseMoveState BaseState;

    void Start()
    {
        EventTire = this.GetEventTire();
        Rigidbody = GetComponent<Rigidbody2D>();
        BaseState = GetComponent<BaseMoveState>();
        BaseState.VerticalMoveState = VerticalMoveState.Idle;
        BaseState.MoveState = MoveState.Idle;

        EventTire.AddEventListener(TireEventType.ControlEvent, OnControlEvent);
        EventTire.AddEventListener(TireEventType.ChangedMoveStateEvent, UpdateHorizontalMoveState);
        EventTire.AddEventListener(TireEventType.ChangedVerticalMoveStateEvent, UpdateVerticalMoveState);
        EventTire.AddEventListener(TireEventType.ChangedJumpStateEvent, OnPlayerChangedJumpStateEvent);
        Rigidbody.gravityScale = 0f;
    }

    void OnDestroy()
    {
        EventTire.RemoveEventListener(TireEventType.ControlEvent, OnControlEvent);
        EventTire.RemoveEventListener(TireEventType.ChangedMoveStateEvent, UpdateHorizontalMoveState);
        EventTire.RemoveEventListener(TireEventType.ChangedVerticalMoveStateEvent, UpdateVerticalMoveState);
        EventTire.RemoveEventListener(TireEventType.ChangedJumpStateEvent, OnPlayerChangedJumpStateEvent);
    }

    void FixedUpdate()
    {
        var horizontalMoveForce = BaseState.HorizontalMoveForce;
        if (horizontalMoveForce != Vector2.zero)
        {
            Rigidbody.AddForce(horizontalMoveForce);
        }

        var verticalMoveForce = BaseState.VerticalMoveForce;
        if(verticalMoveForce != Vector2.zero)
        {
            Rigidbody.AddForce(verticalMoveForce);
        }
    }

    void UpdateVerticalMoveState(object param)
    {
        if (BaseState.VerticalMoveState == VerticalMoveState.Up)
        {
            BaseState.VerticalMoveForce = Vector2.up * BaseState.WalkMoveForse;
        }
        else if (BaseState.VerticalMoveState == VerticalMoveState.Down)
        {
            BaseState.VerticalMoveForce = Vector2.down * BaseState.WalkMoveForse;
        }
        else if (BaseState.VerticalMoveState == VerticalMoveState.Idle)
        {
            BaseState.VerticalMoveForce = Vector2.zero;
        }
    }

    void UpdateHorizontalMoveState(object param)
    {
        if (BaseState.MoveState == MoveState.Left)
        {
            BaseState.HorizontalMoveForce = BaseState.WalkMoveForse * Vector2.left;
        }
        else if (BaseState.MoveState == MoveState.Right)
        {
            BaseState.HorizontalMoveForce = BaseState.WalkMoveForse * Vector2.right;
        }
        else if (BaseState.MoveState == MoveState.Idle)
        {
            BaseState.HorizontalMoveForce = Vector2.zero;
        }
    }

    private void OnControlEvent(object param)
    {
        Dictionary<ControlAction, bool> actions = (Dictionary<ControlAction, bool>)param;
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

        if (actions[ControlAction.WalkUp])
        {
            BaseState.VerticalMoveState = VerticalMoveState.Up;
        }
        else if (actions[ControlAction.WalkDown])
        {
            BaseState.VerticalMoveState = VerticalMoveState.Down;
        }
        else
        {
            BaseState.VerticalMoveState = VerticalMoveState.Idle;
        }

        if (actions[ControlAction.Jump])
        {
            BaseState.JumpState = JumpState.Jump;
        }
    }

    private void OnPlayerChangedJumpStateEvent(object param)
    {
        if(BaseState.JumpState == JumpState.Jump)
        {
            Rigidbody.AddForce((Vector2.up +  (BaseState.Direction > 0 ? Vector2.right : Vector2.left)) * BaseState.JumpForse);
        }
    }

    
}
