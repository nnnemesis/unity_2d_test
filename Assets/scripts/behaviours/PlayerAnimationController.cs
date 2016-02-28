using UnityEngine;
using System.Collections.Generic;

public class PlayerAnimationController : MonoBehaviour, ITireEventListener {

    private Animator Animator;
    private IEventTire EventTire;

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        Animator = GetComponent<Animator>();
        EventTire.AddEventListener(TireEventType.ChangedCurrentWeapon, this);
        EventTire.AddEventListener(TireEventType.WeaponUseStateChangedEvent, this);
    }

    void Start () {
        
    }
	
	void Update ()
    {
    }

    public void OnTireEvent(TireEvent ev)
    {
        if(ev.Type == TireEventType.WeaponUseStateChangedEvent)
        {
            //OnWeaponUseStateChangedEvent((WeaponUseStateChangedEvent)ev);
        }
        else if (ev.Type == TireEventType.ChangedCurrentWeapon)
        {
            OnPlayerChangedCurrentWeapon((ChangedCurrentWeapon)ev);
        }
    }

    private void OnPlayerChangedCurrentWeapon(ChangedCurrentWeapon e)
    {
        //if (e.NewCurrentWeapon != null)
        //{
        //    e.NewCurrentWeapon.GetTire().AddEventListener(TireEventType.WeaponUseStateChangedEvent, this);
        //}
    }

    private void OnWeaponUseStateChangedEvent(WeaponUseStateChangedEvent ev)
    {
        if (ev.NewState == WeaponUseState.Use)
        {
            Animator.SetTrigger("UseAxeWeapon");
        }
    }

}
