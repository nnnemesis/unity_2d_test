using UnityEngine;
using System.Collections.Generic;

public class BaseAnimationController : MonoBehaviour, ITireEventListener {

    private Animator Animator;
    private IEventTire EventTire;

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        Animator = GetComponent<Animator>();
        EventTire.AddEventListener(TireEventType.ChangedMoveStateEvent, this);
        EventTire.AddEventListener(TireEventType.ChangedJumpStateEvent, this);
    }

    void Start () {
        
    }
	
	void Update ()
    {
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
        if (e.NewState == MoveState.Idle)
        {
            Animator.SetTrigger("MoveIdle");
        }
        else if (e.NewState == MoveState.Walk)
        {
            Animator.SetTrigger("MoveWalk");
        }
        else if(e.NewState == MoveState.ShiftWalk)
        {
            Animator.SetTrigger("MoveShiftWalk");
        }
    }

    private void OnPlayerChangedJumpStateEvent(ChangedJumpStateEvent e)
    {
        if (e.NewState == JumpState.Jump)
        {
            Animator.SetTrigger("Jump");
        }
        else if(e.NewState == JumpState.Fall)
        {
            Animator.SetTrigger("Fall");
        }
        else if(e.NewState == JumpState.Grounded)
        {
            Animator.SetTrigger("Grounded");
        }
    }

}
