﻿using UnityEngine;
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
        EventTire.AddEventListener(TireEventType.ChangedJumpStateEvent, this);
    }

    void OnDestroy()
    {
        EventTire.RemoveEventListener(TireEventType.ControlEvent, this);
        EventTire.RemoveEventListener(TireEventType.ChangedMoveStateEvent, this);
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

        if (BaseState.MoveState == MoveState.Walk || BaseState.MoveState == MoveState.ShiftWalk)
        {
            var moveForse = Vector2.right * BaseState.CurrentMoveForse * BaseState.Direction;
            Rigidbody.AddForce(moveForse);
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
            Rigidbody.AddForce(Vector2.up * BaseState.JumpForse);
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
