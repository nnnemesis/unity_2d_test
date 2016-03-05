using UnityEngine;
using System.Collections.Generic;

// controls unit movements when unit on ladder
public class LadderMoveController : MonoBehaviour, ITireEventListener {

    private IEventTire EventTire;
    private Rigidbody2D Rigidbody;
    private BaseMoveState BaseState;

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
        Rigidbody = GetComponent<Rigidbody2D>();
        BaseState = GetComponent<BaseMoveState>();
        BaseState.VerticalMoveState = VerticalMoveState.Idle;
        BaseState.MoveState = MoveState.Idle;

        EventTire.AddEventListener(TireEventType.ControlEvent, this);
        EventTire.AddEventListener(TireEventType.ChangedMoveStateEvent, this);
        EventTire.AddEventListener(TireEventType.ChangedVerticalMoveStateEvent, this);
        EventTire.AddEventListener(TireEventType.ChangedJumpStateEvent, this);
        Rigidbody.gravityScale = 0f;
    }

    void OnDestroy()
    {
        EventTire.RemoveEventListener(TireEventType.ControlEvent, this);
        EventTire.RemoveEventListener(TireEventType.ChangedMoveStateEvent, this);
        EventTire.RemoveEventListener(TireEventType.ChangedVerticalMoveStateEvent, this);
        EventTire.RemoveEventListener(TireEventType.ChangedJumpStateEvent, this);
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

    public void OnTireEvent(TireEvent ev)
    {
        if(ev.Type == TireEventType.ControlEvent)
        {
            OnControlEvent((ControlEvent)ev);
        }
        else if (ev.Type == TireEventType.ChangedMoveStateEvent)
        {
            UpdateHorizontalMoveState();
        }
        else if (ev.Type == TireEventType.ChangedVerticalMoveStateEvent)
        {
            UpdateVerticalMoveState();
        }
        else if (ev.Type == TireEventType.ChangedJumpStateEvent)
        {
            OnPlayerChangedJumpStateEvent((ChangedJumpStateEvent)ev);
        }
    }

    void UpdateVerticalMoveState()
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

    void UpdateHorizontalMoveState()
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

    private void OnControlEvent(ControlEvent e)
    {
        Dictionary<ControlAction, bool> actions = e.Actions;
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

    private void OnPlayerChangedJumpStateEvent(ChangedJumpStateEvent e)
    {
        if(BaseState.JumpState == JumpState.Jump)
        {
            Rigidbody.AddForce((Vector2.up +  (BaseState.Direction > 0 ? Vector2.right : Vector2.left)) * BaseState.JumpForse);
        }
    }

    
}
