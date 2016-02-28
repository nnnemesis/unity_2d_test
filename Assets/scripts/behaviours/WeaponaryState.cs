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
    private Dictionary<WeaponType, SavedWeaponState> KnownWeapons = new Dictionary<WeaponType, SavedWeaponState>();

    public int OwnedWeaponsCount
    {
        get { return OwnedWeapons.Count; }
    }

    public void SaveKnownWeapon(WeaponType weaponType, SavedWeaponState savedWeaponState)
    {
        KnownWeapons[weaponType] = savedWeaponState;
    }

    public SavedWeaponState LoadKnownWeapon(WeaponType weaponType)
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