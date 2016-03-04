using UnityEngine;
using System.Collections.Generic;

public class WeaponaryState : MonoBehaviour
{

    private IEventTire EventTire;

    public int _CurrentWeaponIndex = -1;
    public int CurrentWeaponIndex
    {
        get { return _CurrentWeaponIndex; }
        set
        {
            if(_CurrentWeaponIndex != value)
            {
                _CurrentWeaponIndex = value;
                if(value >= 0)
                {
                    EventTire.SendEvent(new ChangedCurrentWeapon() { NewWeaponType = OwnedWeapons[value], NewWeaponIndex = CurrentWeaponIndex });
                }
                else
                {
                    EventTire.SendEvent(new ChangedCurrentWeapon() { NewWeaponIndex = CurrentWeaponIndex });
                }
                
            }
        }
    }

    public List<WeaponType> OwnedWeapons = new List<WeaponType>();
    private Dictionary<WeaponType, WeaponData> KnownWeapons = new Dictionary<WeaponType, WeaponData>();

    public int OwnedWeaponsCount
    {
        get { return OwnedWeapons.Count; }
    }

    public void SaveKnownWeapon(WeaponData savedWeaponState)
    {
        KnownWeapons[savedWeaponState.WeaponType] = savedWeaponState;
    }

    public WeaponData LoadKnownWeapon(WeaponType weaponType)
    {
        if (KnownWeapons.ContainsKey(weaponType))
        {
            return KnownWeapons[weaponType];
        }
        else
        {
            return null;
        }
    }

    public WeaponType GetOwnedWeaponType(int index)
    {
        return OwnedWeapons[index];
    }

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
    }

}