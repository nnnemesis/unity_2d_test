using UnityEngine;
using System.Collections.Generic;

public class PlayerAnimationController : MonoBehaviour {

    private Animator Animator;
    private IEventTire EventTire;

    void Start ()
    {
        EventTire = this.GetEventTire();
        Animator = GetComponent<Animator>();
        EventTire.AddEventListener(TireEventType.ChangedCurrentWeapon, OnPlayerChangedCurrentWeapon);
        //EventTire.AddEventListener(TireEventType.WeaponUseStateChangedEvent, OnWeaponUseStateChangedEvent);
    }
	
    private void OnPlayerChangedCurrentWeapon(object param)
    {
        //if (e.NewCurrentWeapon != null)
        //{
        //    e.NewCurrentWeapon.GetTire().AddEventListener(TireEventType.WeaponUseStateChangedEvent, this);
        //}
    }

    private void OnWeaponUseStateChangedEvent(object param)
    {
        WeaponUseState newState = (WeaponUseState)param;
        if (newState == WeaponUseState.Use)
        {
            Animator.SetTrigger("UseAxeWeapon");
        }
    }

}
