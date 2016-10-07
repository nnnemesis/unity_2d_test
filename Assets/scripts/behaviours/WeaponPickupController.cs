using UnityEngine;
using System.Collections.Generic;

public class WeaponPickupController : MonoBehaviour {

    private IEventTire EventTire;
    private GameObject WeaponPickup;

    private bool _CanPickupWeapon = false;
    private bool CanPickupWeapon
    {
        get { return _CanPickupWeapon; }
        set
        {
            if(_CanPickupWeapon != value)
            {
                _CanPickupWeapon = value;
                EventTire.SendEvent(TEPath.Up, TireEventType.ChangedCanPickupWeaponEvent, value);
            }
        }
    }

    void OnControlEvent(object param)
    {
        Dictionary<ControlAction, bool> actions = (Dictionary<ControlAction, bool>)param;
        if (CanPickupWeapon && WeaponPickup != null && actions[ControlAction.Use])
        {
            var pickup = WeaponPickup.GetComponent<WeaponPickup>();
            if(pickup != null)
            {
                EventTire.SendEvent(TEPath.Up, TireEventType.WeaponPickupEvent, pickup);
                Destroy(WeaponPickup);
                WeaponPickup = null;
            }
        }
    }

    void Start()
    {
        EventTire = this.GetEventTire();
        EventTire.AddEventListener(TireEventType.ControlEvent, OnControlEvent);
    }

    void OnDestroy()
    {
        EventTire.RemoveEventListener(TireEventType.ControlEvent, OnControlEvent);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<WeaponPickup>() != null)
        {
            WeaponPickup = other.gameObject;
            CanPickupWeapon = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<WeaponPickup>() != null)
        {
            WeaponPickup = null;
            CanPickupWeapon = false;
        }
    }

}
