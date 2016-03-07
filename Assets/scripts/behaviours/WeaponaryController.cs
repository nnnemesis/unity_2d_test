using UnityEngine;
using System.Collections.Generic;

public class WeaponaryController : MonoBehaviour, ITireEventListener {

    private WeaponaryState WeaponaryState;
    private IEventTire EventTire;
    public Transform LeftHandTransform;
    public GameObject CurrentWeapon;

    public void OnTireEvent(TireEvent ev)
    {
        if (ev.Type == TireEventType.SaveWeaponStateEvent)
        {
            OnSaveWeaponStateEvent((SaveWeaponStateEvent)ev);
        }
        else if (ev.Type == TireEventType.LoadWeaponStateEvent)
        {
            OnLoadWeaponStateEvent((LoadWeaponStateEvent)ev);
        }
        else if(ev.Type == TireEventType.ControlEvent)
        {
            OnControlEvent((ControlEvent)ev);
        }
        else if(ev.Type == TireEventType.WeaponPickupEvent)
        {
            OnWeaponPickupEvent((WeaponPickupEvent)ev);
        }
    }

    void OnSaveWeaponStateEvent(SaveWeaponStateEvent ev)
    {
        WeaponaryState.SaveKnownWeapon(ev.WeaponData);
    }

    void OnLoadWeaponStateEvent(LoadWeaponStateEvent ev)
    {
        ev.WeaponData = WeaponaryState.LoadKnownWeapon(ev.WeaponType);
    }

    void OnControlEvent(ControlEvent e)
    {
        Dictionary<ControlAction, bool> actions = e.Actions;
        if (actions[ControlAction.NextWeapon])
        {
            TrySelectNextWeapon();
        }
        else if (actions[ControlAction.PrevWeapon])
        {
            TrySelectPrevWeapon();
        }
    }

    void OnWeaponPickupEvent(WeaponPickupEvent ev)
    {
        var pickup = ev.WeaponPickup;
        var weaponType = pickup.WeaponType;
        var currentWeaponIndex = WeaponaryState.CurrentWeaponIndex;
        // if not current weapon
        if (currentWeaponIndex >= 0 && WeaponaryState.GetOwnedWeaponType(currentWeaponIndex) == weaponType)
        {
            return; // same weapon
        }
        else if(currentWeaponIndex < 0) // no current weapon
        {
            ReplaceWeapon(0, weaponType);
        }
        else
        {
            ReplaceWeapon(currentWeaponIndex, weaponType);
        }
    }

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
        EventTire.AddEventListener(TireEventType.SaveWeaponStateEvent, this);
        EventTire.AddEventListener(TireEventType.LoadWeaponStateEvent, this);
        EventTire.AddEventListener(TireEventType.WeaponPickupEvent, this);

        WeaponaryState = GetComponent<WeaponaryState>();
        if(WeaponaryState.CurrentWeaponIndex >= 0)
        {
            SelectWeapon(WeaponaryState.CurrentWeaponIndex);
        }
    }

    void OnDestroy()
    {
        EventTire.RemoveEventListener(TireEventType.ControlEvent, this);
        EventTire.RemoveEventListener(TireEventType.SaveWeaponStateEvent, this);
        EventTire.RemoveEventListener(TireEventType.LoadWeaponStateEvent, this);
        EventTire.RemoveEventListener(TireEventType.WeaponPickupEvent, this);
    }

    void TrySelectPrevWeapon()
    {
        //Debug.Log("TrySelectPrevWeapon");
        if (WeaponaryState.CurrentWeaponIndex < 0)
        {
            int index = WeaponaryState.GetFirstOwnedWeaponIndex(0);
            if(index >= 0)
            {
                SelectWeapon(0);
            }
        }
        else
        {
            int index = PrevWeaponIndex();
            if(index >= 0)
            {
                SelectWeapon(index);
            }
        }
    }

    void TrySelectNextWeapon()
    {
        //Debug.Log("TrySelectNextWeapon");
        if (WeaponaryState.CurrentWeaponIndex < 0)
        {
            int index = WeaponaryState.GetFirstOwnedWeaponIndex(0);
            if (index >= 0)
            {
                SelectWeapon(0);
            }
        }
        else
        {
            int index = NextWeaponIndex();
            if (index >= 0)
            {
                SelectWeapon(index);
            }
        }
    }

    int NextWeaponIndex()
    {
        int next = WeaponaryState.CurrentWeaponIndex + 1;
        if (next >= WeaponaryState.WeaponarySize)
        {
            next = 0;
        }
        return WeaponaryState.GetFirstOwnedWeaponIndex(next);
    }

    int PrevWeaponIndex()
    {
        int next = WeaponaryState.CurrentWeaponIndex - 1;
        if (next < 0 )
        {
            next = WeaponaryState.WeaponarySize - 1;
        }
        return WeaponaryState.GetLastOwnedWeaponIndex(next);
    }

    void SelectWeapon(int index)
    {
        //Debug.Log("SelectWeapon "+index);
        // destroing previous weapon
        if (CurrentWeapon != null)
        {
            Destroy(CurrentWeapon);
        }
        // loading new weapon
        WeaponType weaponType = WeaponaryState.GetOwnedWeaponType(index);
        GameObject weaponPrefab = PrefabStore.Instance.GetPrefab(GetPrefabIdentifier(weaponType));
        GameObject weapon = (GameObject)Instantiate(weaponPrefab, Vector3.zero, Quaternion.identity);
        SingleEventTireProxy proxy = weapon.AddComponent<SingleEventTireProxy>();
        proxy.Instance = EventTire;
        weapon.transform.SetParent(LeftHandTransform, false);
        CurrentWeapon = weapon;
        WeaponaryState.CurrentWeaponIndex = index;
    }

    void ReplaceWeapon(int index, WeaponType NewWeaponType)
    {
        // destroing previous weapon
        if (CurrentWeapon != null)
        {
            Destroy(CurrentWeapon);
        }
        // loading new weapon
        WeaponaryState.SetOwnedWeaponType(index, NewWeaponType);
        SelectWeapon(index);
    }

    PrefabIdentifier GetPrefabIdentifier(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Axe:
                return PrefabIdentifier.WeaponAxe;
            case WeaponType.Stick:
                return PrefabIdentifier.WeaponStick;
            case WeaponType.Pistol:
                return PrefabIdentifier.WeaponPistol;
            case WeaponType.Automat:
                return PrefabIdentifier.WeaponAutomat;
        }
        return PrefabIdentifier.None;
    }
	
}
