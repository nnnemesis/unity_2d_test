using UnityEngine;
using System.Collections.Generic;

public enum WeaponType
{
    Hand,
    Axe,
    Pistol,
    Automat
}

public enum WeaponUseState
{
    Idle,
    Use,
    Reload
}

public class WeaponCurrentTotalAmmoChangedEvent : TireEvent
{
    public int NewState;

    public WeaponCurrentTotalAmmoChangedEvent()
    {
        Type = TireEventType.WeaponCurrentTotalAmmoChangedEvent;
    }

    public override string ToString()
    {
        return "Type " + Type + " NewState " + NewState;
    }
}

public class WeaponCurrentAmmoChangedEvent : TireEvent
{
    public int NewState;

    public WeaponCurrentAmmoChangedEvent()
    {
        Type = TireEventType.WeaponCurrentAmmoChangedEvent;
    }

    public override string ToString()
    {
        return "Type " + Type + " NewState " + NewState;
    }
}

public class WeaponUseStateChangedEvent : TireEvent
{
    public WeaponUseState NewState;

    public WeaponUseStateChangedEvent()
    {
        Type = TireEventType.WeaponUseStateChangedEvent;
    }

    public override string ToString()
    {
        return "Type " + Type + " WeaponUseState " + NewState;
    }

}