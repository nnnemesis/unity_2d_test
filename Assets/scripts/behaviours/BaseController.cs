using UnityEngine;
using System.Collections.Generic;

public class BaseController : MonoBehaviour, ITireEventListener {

    private BaseState BaseState;
    private IEventTire EventTire;

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        BaseState = GetComponent<BaseState>();
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
        EventTire.AddEventListener(TireEventType.ChangedMoveStateEvent, this);
        EventTire.AddEventListener(TireEventType.ChangedJumpStateEvent, this);
    }

    void FixedUpdate()
    {
        if(BaseState.JumpState != JumpState.Grounded)
        {
            float velocityY = BaseState.Rigidbody.velocity.y;
            if (velocityY < 0)    // raycast only when fall!
            {
                if(velocityY < -5)
                {
                    BaseState.JumpState = JumpState.Fall;
                }                    
                Vector2 Position = BaseState.Transform.position;
                bool grounded = Physics2D.Linecast(Position, Position + BaseState.GroundCheckVector, LayerMask.GetMask("Ground"));
                if (grounded)
                {
                    BaseState.JumpState = JumpState.Grounded;
                }
            }
        }                    

        if (BaseState.MoveState == MoveState.Walk || BaseState.MoveState == MoveState.ShiftWalk)
        {
            var moveForse = Vector2.right * BaseState.CurrentMoveForse * BaseState.Direction;
            BaseState.Rigidbody.AddForce(moveForse);
        }
            
    }

    public void OnTireEvent(TireEvent ev)
    {
        //Debug.Log("PLAYER controller on tire event "+ev.Type);
        if(ev.Type == TireEventType.ControlEvent)
        {
            OnControlEvent((ControlEvent)ev);
        }
        else if(ev.Type == TireEventType.ChangedMoveStateEvent)
        {
            OnPlayerChangedMoveStateEvent((ChangedMoveStateEvent)ev);
        }
        else if (ev.Type == TireEventType.ChangedJumpStateEvent)
        {
            OnPlayerChangedJumpStateEvent((ChangedJumpStateEvent)ev);
        }
    }

    private void OnPlayerChangedMoveStateEvent(ChangedMoveStateEvent e)
    {
        if (BaseState.MoveState == MoveState.Walk)
            BaseState.CurrentMoveForse = BaseState.WalkMoveForse;
        else if (BaseState.MoveState == MoveState.ShiftWalk)
            BaseState.CurrentMoveForse = BaseState.ShiftWalkMoveForse;
        else if (BaseState.MoveState == MoveState.Idle)
            BaseState.CurrentMoveForse = 0f;
    }

    private void OnPlayerChangedJumpStateEvent(ChangedJumpStateEvent e)
    {
        if(BaseState.JumpState == JumpState.Jump)
        {
            BaseState.Rigidbody.AddForce(Vector2.up * BaseState.JumpForse);
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
            if (actions[ControlAction.Shift])
            {
                BaseState.MoveState = MoveState.ShiftWalk;
            }
            else
            {
                BaseState.MoveState = MoveState.Walk;
            }
        }
        else if (actions[ControlAction.WalkBack])
        {
            BaseState.Direction = -1;
            var rotation = transform.rotation;
            rotation.SetLookRotation(Vector3.back);
            transform.rotation = rotation;
            if (actions[ControlAction.Shift])
            {
                BaseState.MoveState = MoveState.ShiftWalk;
            }
            else
            {
                BaseState.MoveState = MoveState.Walk;
            }
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
