﻿using UnityEngine;
using System.Collections;

public class HitWeaponController : MonoBehaviour, IWeapon, ITireEventListener
{
    public WeaponState State;
    private IEventTire EventTire;

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        State = GetComponent<WeaponState>();
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

    public void StartUsing()
    {
        if (State.UseStateDone && State.UseState == WeaponUseState.Idle)
        {
            State.UseState = WeaponUseState.Use;
            State.UseStateDone = false;
            Invoke("UsingDone", State.OneUseTime);
        }            
    }

    private void UsingDone()
    {
        State.UseStateDone = true;
        if (State.UseState == WeaponUseState.Use)
            State.UseState = WeaponUseState.Idle;
    }

    public void StopUsing()
    {
        // no reaction, axe will travel to idle after one use
    }

    public void Recharge()
    {
        // no reaction, axe dosent need recharging
    }

    public IEventTire GetTire()
    {
        return EventTire;
    }

    public void OnTireEvent(TireEvent ev) { }

}