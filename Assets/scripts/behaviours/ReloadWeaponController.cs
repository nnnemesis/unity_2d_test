using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReloadWeaponController : MonoBehaviour
{
    private WeaponState State;
    private IEventTire EventTire;

    void Start()
    {
        State = GetComponent<WeaponState>();
        EventTire = this.GetEventTire();
        EventTire.AddEventListener(TireEventType.ControlEvent, OnControlEvent);
        EventTire.AddEventListener(TireEventType.AmmoPickupEvent, OnAmmoPickupEvent);
    }

    void OnDestroy()
    {
        EventTire.RemoveEventListener(TireEventType.ControlEvent, OnControlEvent);
        EventTire.RemoveEventListener(TireEventType.AmmoPickupEvent, OnAmmoPickupEvent);
    }

    void OnAmmoPickupEvent(object param)
    {
        var ammoPickup = (AmmoPickup)param;
        var weaponType = ammoPickup.WeaponType;
        // if not current weapon
        if (State.WeaponType == weaponType)
        {
            State.CurrentTotalAmmo = Mathf.Min(State.CurrentTotalAmmo + ammoPickup.AmmoCount, State.MaxTotalAmmo);
        }
    }

    void OnControlEvent(object param)
    {
        Dictionary<ControlAction, bool> actions = (Dictionary<ControlAction, bool>)param;
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