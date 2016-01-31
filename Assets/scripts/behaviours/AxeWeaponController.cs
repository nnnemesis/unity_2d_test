using UnityEngine;
using System.Collections;

public class AxeWeaponController : MonoBehaviour, IWeapon, ITireEventListener
{
    public WeaponState State;
    private IEventTire EventTire;

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        State = GetComponent<WeaponState>();
    }

    public void StartUsing()
    {
        if (State.UseStateDone && State.UseState == WeaponUseState.Idle)
        {
            Debug.Log("Use axe!");
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
        return GetComponent<IEventTire>();
    }

    public void OnTireEvent(TireEvent ev) { }

}