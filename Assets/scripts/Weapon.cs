using UnityEngine;
using System.Collections.Generic;
using System;

public enum WeaponType
{
    None,
    Axe,
    Stick,
    Pistol,
    Automat
}

public enum WeaponUseState
{
    Idle,
    Use,
    Reload
}

[Serializable]
public class WeaponData
{
    public WeaponType WeaponType = WeaponType.None;
    public int CurrentMagazineAmmo = 0;
    public int CurrentTotalAmmo = 0;
    public int MaxTotalAmmo = 0;
    public int MaxMagazineAmmo = 0;
    public float ReloadTime = 0;
    public float OneUseTime = 0f;
    public float DamageAmount = 0f;

    public override string ToString()
    {
        return "WeaponType " + WeaponType + " CurrentMagazineAmmo " + CurrentMagazineAmmo + " CurrentTotalAmmo "+ CurrentTotalAmmo
            + " MaxTotalAmmo " + MaxTotalAmmo + " MaxMagazineAmmo " + MaxMagazineAmmo + " ReloadTime " + ReloadTime + " OneUseTime " + OneUseTime 
            + " DamageAmount " + DamageAmount;
    }

}

[Serializable]
public class CardWeapon
{
    public WeaponType WeaponType;
    public int MaxTotalAmmo = 0;
    public int MaxMagazineAmmo = 0;
    public float ReloadTime = 0;
    public float OneUseTime = 0.5f;
    public float DamageAmount = 20f;
    public Sprite Icon;
}

public class WeaponPickupEvent : TireEvent
{
    public WeaponPickup WeaponPickup;

    public WeaponPickupEvent() { Type = TireEventType.WeaponPickupEvent; }

    public override string ToString()
    {
        return "Type " + Type + " WeaponPickup " + WeaponPickup;
    }
}

public class AmmoPickupEvent : TireEvent
{
    public AmmoPickup AmmoPickup;

    public AmmoPickupEvent() { Type = TireEventType.AmmoPickupEvent; }

    public override string ToString()
    {
        return "Type " + Type + " AmmoPickup " + AmmoPickup;
    }
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

public class LoadWeaponStateEvent : TireEvent
{
    public WeaponType WeaponType;
    public WeaponData WeaponData;

    public LoadWeaponStateEvent()
    {
        Type = TireEventType.LoadWeaponStateEvent;
    }

    public override string ToString()
    {
        return "Type " + Type + " WeaponData " + WeaponData;
    }
}

public class SaveWeaponStateEvent : TireEvent
{
    public WeaponData WeaponData;

    public SaveWeaponStateEvent()
    {
        Type = TireEventType.SaveWeaponStateEvent;
    }

    public override string ToString()
    {
        return "Type " + Type + " WeaponData " + WeaponData;
    }

}