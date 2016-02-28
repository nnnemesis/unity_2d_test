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
    }

    void OnSaveWeaponStateEvent(SaveWeaponStateEvent ev)
    {
        WeaponaryState.SaveKnownWeapon(ev.SavedWeaponState.WeaponType, ev.SavedWeaponState);
    }

    void OnLoadWeaponStateEvent(LoadWeaponStateEvent ev)
    {
        ev.SavedWeaponState = WeaponaryState.LoadKnownWeapon(ev.SavedWeaponState.WeaponType);
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

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
        EventTire.AddEventListener(TireEventType.SaveWeaponStateEvent, this);
        EventTire.AddEventListener(TireEventType.LoadWeaponStateEvent, this);

        WeaponaryState = GetComponent<WeaponaryState>();
        if(WeaponaryState.CurrentWeaponIndex >= 0)
        {
            SelectWeapon(WeaponaryState.CurrentWeaponIndex);
        }
    }

    void TrySelectPrevWeapon()
    {
        Debug.Log("TrySelectPrevWeapon");
        var OwnedWeaponsCount = WeaponaryState.OwnedWeaponsCount;
        if (OwnedWeaponsCount == 1 && WeaponaryState.CurrentWeaponIndex < 0)
        {
            SelectWeapon(0);    
        }
        else if(OwnedWeaponsCount > 1)
        {
            SelectWeapon(PrevWeaponIndex());
        }
    }

    void TrySelectNextWeapon()
    {
        Debug.Log("TrySelectNextWeapon");
        var OwnedWeaponsCount = WeaponaryState.OwnedWeaponsCount;
        if (OwnedWeaponsCount == 1 && WeaponaryState.CurrentWeaponIndex < 0)
        {
            SelectWeapon(0);
        }
        else if (OwnedWeaponsCount > 1)
        {
            SelectWeapon(NextWeaponIndex());
        }
    }

    int NextWeaponIndex()
    {
        int next = WeaponaryState.CurrentWeaponIndex + 1;
        if (next >= WeaponaryState.OwnedWeaponsCount)
        {
            next = 0;
        }
        return next;
    }

    int PrevWeaponIndex()
    {
        int next = WeaponaryState.CurrentWeaponIndex - 1;
        if (next < 0 )
        {
            next = WeaponaryState.OwnedWeaponsCount - 1;
        }
        return next;
    }

    void SelectWeapon(int index)
    {
        Debug.Log("SelectWeapon "+index);
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

    //void Start() {
    //    //GameObject testWeapon = (GameObject)Instantiate(InitWeapon, Vector3.zero, Quaternion.identity);
    //    //SingleEventTireProxy proxy = testWeapon.AddComponent<SingleEventTireProxy>();
    //    //proxy.Instance = EventTire;
    //    //testWeapon.transform.SetParent(LeftHandTransform, false);
    //    //WeaponaryState.CurrentWeapon = testWeapon;
    //}
	
}
