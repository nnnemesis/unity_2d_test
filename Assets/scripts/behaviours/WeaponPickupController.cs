using UnityEngine;
using System.Collections.Generic;

public class WeaponPickupController : MonoBehaviour, ITireEventListener {

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
                EventTire.SendEvent(new ChangedCanPickupWeaponEvent() { NewState = value });
            }
        }
    }

    public void OnTireEvent(TireEvent ev)
    {
        if(ev.Type == TireEventType.ControlEvent)
        {
            OnControlEvent((ControlEvent)ev);
        }
    }

    void OnControlEvent(ControlEvent e)
    {
        Dictionary<ControlAction, bool> actions = e.Actions;
        if (CanPickupWeapon && WeaponPickup != null && actions[ControlAction.Use])
        {
            var pickup = WeaponPickup.GetComponent<WeaponPickup>();
            if(pickup != null)
            {
                EventTire.SendEvent(new WeaponPickupEvent() { WeaponPickup = pickup });
                Destroy(WeaponPickup);
                WeaponPickup = null;
            }
        }
    }

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
    }

    void OnDestroy()
    {
        EventTire.RemoveEventListener(TireEventType.ControlEvent, this);
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
