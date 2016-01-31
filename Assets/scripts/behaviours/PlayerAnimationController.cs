using UnityEngine;
using System.Collections.Generic;

public class PlayerAnimationController : MonoBehaviour, ITireEventListener {

    private Animator Animator;
    private IEventTire EventTire;

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        Animator = GetComponent<Animator>();
        EventTire.AddEventListener(TireEventType.PlayerChangedMoveStateEvent, this);
        EventTire.AddEventListener(TireEventType.PlayerChangedJumpStateEvent, this);
        EventTire.AddEventListener(TireEventType.PlayerChangedCurrentWeapon, this);
    }

    void Start () {
        
    }
	
	void Update ()
    {
    }

    public void OnTireEvent(TireEvent ev)
    {
        //Debug.Log("PLAYER controller on tire event "+ev.Type);
        if(ev.Type == TireEventType.PlayerChangedMoveStateEvent)
        {
            OnPlayerChangedMoveStateEvent((PlayerChangedMoveStateEvent)ev);
        }
        else if (ev.Type == TireEventType.PlayerChangedJumpStateEvent)
        {
            OnPlayerChangedJumpStateEvent((PlayerChangedJumpStateEvent)ev);
        }
        else if(ev.Type == TireEventType.WeaponUseStateChangedEvent)
        {
            OnWeaponUseStateChangedEvent((WeaponUseStateChangedEvent)ev);
        }
        else if (ev.Type == TireEventType.PlayerChangedCurrentWeapon)
        {
            OnPlayerChangedCurrentWeapon((PlayerChangedCurrentWeapon)ev);
        }
    }

    private void OnPlayerChangedCurrentWeapon(PlayerChangedCurrentWeapon e)
    {
        if (e.NewCurrentWeapon != null)
        {
            Debug.Log("WeaponUseStateChangedEvent Listener added");
            e.NewCurrentWeapon.GetTire().AddEventListener(TireEventType.WeaponUseStateChangedEvent, this);
        }
    }

    private void OnWeaponUseStateChangedEvent(WeaponUseStateChangedEvent ev)
    {
        if (ev.NewState == WeaponUseState.Use)
        {
            Animator.SetTrigger("UseAxeWeapon");
        }
    }

    private void OnPlayerChangedMoveStateEvent(PlayerChangedMoveStateEvent e)
    {
        if (e.NewState == PlayerMoveState.Idle)
        {
            Animator.SetTrigger("MoveIdle");
        }
        else if (e.NewState == PlayerMoveState.Walk)
        {
            Animator.SetTrigger("MoveWalk");
        }
        else if(e.NewState == PlayerMoveState.ShiftWalk)
        {
            Animator.SetTrigger("MoveShiftWalk");
        }
    }

    private void OnPlayerChangedJumpStateEvent(PlayerChangedJumpStateEvent e)
    {
        if (e.NewState == PlayerJumpState.Jump)
        {
            Animator.SetTrigger("Jump");
        }
        else if(e.NewState == PlayerJumpState.Fall)
        {
            Animator.SetTrigger("Fall");
        }
        else if(e.NewState == PlayerJumpState.Grounded)
        {
            Animator.SetTrigger("Grounded");
        }
    }

}
