using UnityEngine;
using System.Collections.Generic;
using System;

public class WeaponCards : SomeCards<WeaponType, CardWeapon>
{
    
    public static void FillWithDefault(WeaponType weaponType, WeaponData weaponData)
    {
        var card = GetCard(weaponType);
        weaponData.WeaponType = weaponType;
        weaponData.MaxMagazineAmmo = card.MaxMagazineAmmo;
        weaponData.MaxTotalAmmo = card.MaxTotalAmmo;
        weaponData.OneUseTime = card.OneUseTime;
        weaponData.ReloadTime = card.ReloadTime;
        weaponData.DamageAmount = card.DamageAmount;
    }

}