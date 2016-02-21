using UnityEngine;
using System.Collections.Generic;

// controls unit movements when unit on ladder
public class LadderMoveController : MonoBehaviour, ITireEventListener {

    private IEventTire EventTire;
    private Rigidbody2D Rigidbody;
    private BaseMoveState BaseState;

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        Rigidbody = GetComponent<Rigidbody2D>();
        BaseState = GetComponent<BaseMoveState>();
    }

    void Start()
    {
        BaseState.VerticalMoveState = VerticalMoveState.Idle;
        BaseState.MoveState = MoveState.Idle;
    }

    void OnEnable()
    {
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
        EventTire.AddEventListener(TireEventType.ChangedMoveStateEvent, this);
        EventTire.AddEventListener(TireEventType.ChangedJumpStateEvent, this);
        Rigidbody.gravityScale = 0f;        
    }

    void OnDisable()
    {
        EventTire.RemoveEventListener(TireEventType.ControlEvent, this);
        EventTire.RemoveEventListener(TireEventType.ChangedMoveStateEvent, this);
        EventTire.RemoveEventListener(TireEventType.ChangedJumpStateEvent, this);
    }

    void FixedUpdate()
    {
        if (BaseState.MoveState == MoveState.Walk)
        {
            Rigidbody.AddForce(Vector2.right * BaseState.WalkMoveForse * BaseState.Direction);
        }

        if(BaseState.VerticalMoveState == VerticalMoveState.Up)
        {
            Rigidbody.AddForce(Vector2.up * BaseState.WalkMoveForse);
        }
        else if (BaseState.VerticalMoveState == VerticalMoveState.Down)
        {
            Rigidbody.AddForce(Vector2.down * BaseState.WalkMoveForse);
        }
    }

    public void OnTireEvent(TireEvent ev)
    {
        if(ev.Type == TireEventType.ControlEvent)
        {
            OnControlEvent((ControlEvent)ev);
        }
        else if (ev.Type == TireEventType.ChangedJumpStateEvent)
        {
            OnPlayerChangedJumpStateEvent((ChangedJumpStateEvent)ev);
        }
    }

    private void OnControlEvent(ControlEvent e)
    {
        Dictionary<ControlAction, bool> actions = e.Actions;
        if (actions[ControlAction.WalkForward])
        {
            BaseState.Direction = 1;
            var rotation = transform.rotation;
            rotation.SetLookRotation(Vector3.forward);
            transform.rotation = rotation;
            BaseState.MoveState = MoveState.Walk;
        }
        else if (actions[ControlAction.WalkBack])
        {
            BaseState.Direction = -1;
            var rotation = transform.rotation;
            rotation.SetLookRotation(Vector3.back);
            transform.rotation = rotation;
            BaseState.MoveState = MoveState.Walk;
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
