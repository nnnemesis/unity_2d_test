using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReloadWeaponController : MonoBehaviour, ITireEventListener
{
    private WeaponState State;
    private IEventTire EventTire;

    void Start()
    {
        State = GetComponent<WeaponState>();
        EventTire = GetComponent<IEventTire>();
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
        EventTire.AddEventListener(TireEventType.AmmoPickupEvent, this);
    }

    public void OnTireEvent(TireEvent ev)
    {
        if (ev.Type == TireEventType.ControlEvent)
        {
            OnControlEvent((ControlEvent)ev);
        }
        else if (ev.Type == TireEventType.AmmoPickupEvent)
        {
            OnAmmoPickupEvent((AmmoPickupEvent)ev);
        }
    }

    void OnAmmoPickupEvent(AmmoPickupEvent ev)
    {
        var ammoPickup = ev.AmmoPickup;
        var weaponType = ammoPickup.WeaponType;
        // if not current weapon
        if (State.WeaponType == weaponType)
        {
            State.CurrentTotalAmmo = Mathf.Min(State.CurrentTotalAmmo + ammoPickup.AmmoCount, State.MaxTotalAmmo);
        }
    }

    void OnControlEvent(ControlEvent e)
    {
        Dictionary<ControlAction, bool> actions = e.Actions;
        if (actions[ControlAction.Recharge])
        {
            Recharge();
        }
    }

    void Recharge()
    {
        if (State.UseState != WeaponUseState.Reload)
        {
            State.UseState = WeaponUseState.Reload;
            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(State.ReloadTime);
        DoRecharge();
        State.UseState = WeaponUseState.Idle;
    }

    void DoRecharge()
    {
        var currentTotal = State.CurrentTotalAmmo;
        if (currentTotal > 0)
        {
            var currentInMagazine = State.CurrentMagazineAmmo;
            var restTotal = currentTotal + currentInMagazine - State.MaxMagazineAmmo;
            if (restTotal >= 0)
            {
                State.CurrentMagazineAmmo = State.MaxMagazineAmmo;
                State.CurrentTotalAmmo = restTotal;
            }
            else
            {
                State.CurrentMagazineAmmo = currentTotal + currentInMagazine;
                State.CurrentTotalAmmo = 0;
            }
        }
    }

}