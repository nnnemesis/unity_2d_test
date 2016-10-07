using UnityEngine;
using System.Collections.Generic;

public class AmmoPickupController : MonoBehaviour {

    private IEventTire EventTire;
    private WeaponaryState WeaponaryState;
    private GameObject AmmoPickup;

    private bool _CanPickupWeapon = false;
    private bool CanPickupWeapon
    {
        get { return _CanPickupWeapon; }
        set
        {
            if(_CanPickupWeapon != value)
            {
                _CanPickupWeapon = value;
                EventTire.SendEvent(TEPath.Up, TireEventType.ChangedCanPickupAmmoEvent, value);
            }
        }
    }

    void OnControlEvent(object param)
    {
        Dictionary<ControlAction, bool> actions = (Dictionary<ControlAction, bool>)param;
        if (CanPickupWeapon && AmmoPickup != null && actions[ControlAction.Use])
        {
            var pickup = AmmoPickup.GetComponent<AmmoPickup>();
            if(pickup != null)
            {
                EventTire.SendEvent(TEPath.Up, TireEventType.AmmoPickupEvent, pickup);
                Destroy(AmmoPickup);
                AmmoPickup = null;
            }
        }
    }

    void OnAmmoPickupEvent(object param)
    {
        AmmoPickup ammoPickup = (AmmoPickup)param;
        var weaponType = ammoPickup.WeaponType;
        // if not current weapon
        if ((WeaponaryState.CurrentWeaponIndex < 0) 
            || (WeaponaryState.GetOwnedWeaponType(WeaponaryState.CurrentWeaponIndex) != weaponType))
        {
            var weaponData = WeaponaryState.LoadKnownWeapon(weaponType);
            if (weaponData == null)
            {
                weaponData = new WeaponData();
                WeaponCards.FillWithDefault(weaponType, weaponData);
            }
            weaponData.CurrentTotalAmmo = Mathf.Min(weaponData.CurrentTotalAmmo + ammoPickup.AmmoCount, weaponData.MaxTotalAmmo);
            WeaponaryState.SaveKnownWeapon(weaponData);
        }
    }

    void Start()
    {
        EventTire = this.GetEventTire();
        EventTire.AddEventListener(TireEventType.ControlEvent, OnControlEvent);
        EventTire.AddEventListener(TireEventType.AmmoPickupEvent, OnAmmoPickupEvent);
        WeaponaryState = GetComponent<WeaponaryState>();
    }

    void OnDestroy()
    {
        EventTire.RemoveEventListener(TireEventType.ControlEvent, OnControlEvent);
        EventTire.RemoveEventListener(TireEventType.AmmoPickupEvent, OnAmmoPickupEvent);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.LogWarning("OnTriggerEnter");
        if (other.GetComponent<AmmoPickup>())
        {
            AmmoPickup = other.gameObject;
            CanPickupWeapon = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.LogWarning("OnTriggerEnter");
        if (other.GetComponent<AmmoPickup>())
        {
            AmmoPickup = null;
            CanPickupWeapon = false;
        }
    }

}
