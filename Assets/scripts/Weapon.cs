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