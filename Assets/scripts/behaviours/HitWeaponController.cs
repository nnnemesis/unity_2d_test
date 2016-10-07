using UnityEngine;
using System.Collections.Generic;

public class HitWeaponController : MonoBehaviour
{
    private WeaponState State;
    private IEventTire EventTire;

    void Start()
    {
        State = GetComponent<WeaponState>();
        EventTire = this.GetEventTire();
        EventTire.AddEventListener(TireEventType.ControlEvent, OnControlEvent);
    }

    void OnControlEvent(object param)
    {
        Dictionary<ControlAction, bool> actions = (Dictionary<ControlAction, bool>)param;
        if (actions[ControlAction.MainAttack])
        {
            StartUsing();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.LogWarning("OnTriggerEnter");
        if(State.UseState == WeaponUseState.Use)
        {
            var damagable = other.gameObject.GetComponent<IDamageble>();
            if(damagable != null)
            {
                damagable.InflictDamage(DamageType.Hit, State.DamageAmount);
            }
        }
            
    }

    void StartUsing()
    {
        if (State.UseState == WeaponUseState.Idle)
        {
            State.UseState = WeaponUseState.Use;
            Invoke("UsingDone", State.OneUseTime);
        }            
    }

    void UsingDone()
    {
        if (State.UseState == WeaponUseState.Use)
            State.UseState = WeaponUseState.Idle;
    }

}