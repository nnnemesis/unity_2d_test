using UnityEngine;
using System.Collections.Generic;

public class BaseMoveAnimationController : MonoBehaviour, ITireEventListener {

    private Animator Animator;
    private IEventTire EventTire;

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
        Animator = GetComponent<Animator>();
        EventTire.AddEventListener(TireEventType.ChangedMoveStateEvent, this);
        EventTire.AddEventListener(TireEventType.ChangedJumpStateEvent, this);
    }

    public void OnTireEvent(TireEvent ev)
    {
        if(ev.Type == TireEventType.ChangedMoveStateEvent)
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
        var newState = e.NewState;
        if (newState == MoveState.Idle)
        {
            Animator.SetTrigger("MoveIdle");
        }
        else if (newState == MoveState.Left || newState == MoveState.Right)
        {
            if (e.ShiftWalk)
            {
                Animator.SetTrigger("MoveShiftWalk");
            }
            else
            {
                Animator.SetTrigger("MoveWalk");
            }
            
        }
    }

    private void OnPlayerChangedJumpStateEvent(ChangedJumpStateEvent e)
    {
        var newState = e.NewState;
        if (newState == JumpState.Jump)
        {
            Animator.SetTrigger("Jump");
        }
        else if(newState == JumpState.Fall)
        {
            Animator.SetTrigger("Fall");
        }
        else if(newState == JumpState.Grounded)
        {
            Animator.SetTrigger("Grounded");
        }
    }

}
