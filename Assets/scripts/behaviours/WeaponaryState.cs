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
                    EventTire.SendEvent(TEPath.Up, TireEventType.ChangedCurrentWeapon, new object[] {OwnedWeapons[value], CurrentWeaponIndex });
                }
                else
                {
                    EventTire.SendEvent(TEPath.Up, TireEventType.ChangedCurrentWeapon, new object[] { null, CurrentWeaponIndex });
                }
                
            }
        }
    }

    public List<WeaponType> OwnedWeapons = new List<WeaponType>();
    private Dictionary<WeaponType, WeaponData> KnownWeapons = new Dictionary<WeaponType, WeaponData>();

    public int WeaponarySize
    {
        get { return OwnedWeapons.Count; }
    }

    public int GetFirstOwnedWeaponIndex(int beginIndex)
    {
        for (int i = beginIndex; i < OwnedWeapons.Count; i += 1)
        {
            if (OwnedWeapons[i] != WeaponType.None)
                return i;
        }
        return -1;
    }

    public int GetLastOwnedWeaponIndex(int beginIndex)
    {
        for (int i = beginIndex; i >= 0; i -= 1)
        {
            if (OwnedWeapons[i] != WeaponType.None)
                return i;
        }
        return -1;
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

    public void SetOwnedWeaponType(int index, WeaponType WeaponType)
    {
        Debug.LogWarning("index " + index + " WeaponType " + WeaponType);
        OwnedWeapons[index] = WeaponType;
        EventTire.SendEvent(TEPath.Up, TireEventType.ReplacedCurrentWeaponEvent, new object[] {index, WeaponType });
    }

    void Start()
    {
        EventTire = this.GetEventTire();
        OwnedWeapons.Add(WeaponType.None);
        OwnedWeapons.Add(WeaponType.None);
        OwnedWeapons.Add(WeaponType.None);
        OwnedWeapons.Add(WeaponType.None);
        OwnedWeapons.Add(WeaponType.None);
    }

}