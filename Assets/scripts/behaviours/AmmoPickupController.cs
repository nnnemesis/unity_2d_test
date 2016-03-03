using UnityEngine;
using System.Collections.Generic;

public class AmmoPickupController : MonoBehaviour, ITireEventListener {

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
                EventTire.SendEvent(new ChangedCanPickupAmmoEvent() { NewState = value });
            }
        }
    }

    public void OnTireEvent(TireEvent ev)
    {
        if(ev.Type == TireEventType.ControlEvent)
        {
            OnControlEvent((ControlEvent)ev);
        }
        else if (ev.Type == TireEventType.AmmoPickupEvent)
        {
            OnAmmoPickupEvent((AmmoPickupEvent)ev);
        }
    }

    void OnControlEvent(ControlEvent e)
    {
        Dictionary<ControlAction, bool> actions = e.Actions;
        if (CanPickupWeapon && AmmoPickup != null && actions[ControlAction.Use])
        {
            var pickup = AmmoPickup.GetComponent<AmmoPickupProxy>();
            if(pickup != null)
            {
                EventTire.SendEvent(new AmmoPickupEvent() { AmmoPickup = pickup.AmmoPickup });
                Destroy(AmmoPickup);
                AmmoPickup = null;
            }
        }
    }

    void OnAmmoPickupEvent(AmmoPickupEvent ev)
    {
        var ammoPickup = ev.AmmoPickup;
        var weaponType = ammoPickup.WeaponType;
        // if not current weapon
        if (WeaponaryState.GetOwnedWeaponType(WeaponaryState.CurrentWeaponIndex) != weaponType)
        {
            SavedWeaponState savedWeaponState = WeaponaryState.LoadKnownWeapon(weaponType);
            if (savedWeaponState == null)
            {
                savedWeaponState = WeaponConstants.Instance.GetSavedWeaponState(weaponType);
            }
            savedWeaponState.CurrentTotalAmmo = Mathf.Min(savedWeaponState.CurrentTotalAmmo + ammoPickup.AmmoCount, savedWeaponState.MaxTotalAmmo);
            WeaponaryState.SaveKnownWeapon(weaponType, savedWeaponState);
        }
    }

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
        EventTire.AddEventListener(TireEventType.AmmoPickupEvent, this);
        WeaponaryState = GetComponent<WeaponaryState>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.LogWarning("OnTriggerEnter");
        if (other.CompareTag("AmmoPickup"))
        {
            AmmoPickup = other.gameObject;
            CanPickupWeapon = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.LogWarning("OnTriggerEnter");
        if (other.CompareTag("AmmoPickup"))
        {
            AmmoPickup = null;
            CanPickupWeapon = false;
        }
    }

}
