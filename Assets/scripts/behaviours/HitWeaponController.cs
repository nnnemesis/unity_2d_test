using UnityEngine;
using System.Collections.Generic;

public class HitWeaponController : MonoBehaviour, ITireEventListener
{
    private WeaponState State;
    private IEventTire EventTire;

    void Awake()
    {
        State = GetComponent<WeaponState>();
    }

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
    }

    public void OnTireEvent(TireEvent ev)
    {
        if (ev.Type == TireEventType.ControlEvent)
        {
            OnControlEvent((ControlEvent)ev);
        }
    }

    void OnControlEvent(ControlEvent e)
    {
        Dictionary<ControlAction, bool> actions = e.Actions;
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