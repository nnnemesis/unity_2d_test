using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponSaveLoadController : MonoBehaviour
{
    private WeaponState WeaponState;
    private IEventTire EventTire;

    void Start()
    {
        WeaponState = GetComponent<WeaponState>();
        EventTire = GetComponent<IEventTire>();
        LoadWeaponState();
    }

    void OnDestroy()
    {
        SaveWeaponState();
    }

    void LoadWeaponState()
    {
        var ev = new LoadWeaponStateEvent() { WeaponType = WeaponState.WeaponType };
        EventTire.SendEvent(ev);
        var weaponData = ev.WeaponData;
        if (weaponData == null)
        {
            weaponData = new WeaponData();
            WeaponCards.FillWithDefault(WeaponState.WeaponType, weaponData);
        }
        WeaponState.CurrentMagazineAmmo = weaponData.CurrentMagazineAmmo;
        WeaponState.CurrentTotalAmmo = weaponData.CurrentTotalAmmo;
        WeaponState.OneUseTime = weaponData.OneUseTime;
        WeaponState.ReloadTime = weaponData.ReloadTime;
        WeaponState.MaxMagazineAmmo = weaponData.MaxMagazineAmmo;
        WeaponState.MaxTotalAmmo = weaponData.MaxTotalAmmo;
        WeaponState.DamageAmount = weaponData.DamageAmount;
    }

    void SaveWeaponState()
    {
        var weaponData = new WeaponData() {
            WeaponType = WeaponState.WeaponType
        , CurrentMagazineAmmo = WeaponState.CurrentMagazineAmmo
        , CurrentTotalAmmo = WeaponState.CurrentTotalAmmo
        , DamageAmount = WeaponState.DamageAmount
        , MaxMagazineAmmo = WeaponState.MaxMagazineAmmo
        , MaxTotalAmmo = WeaponState.MaxTotalAmmo
        , OneUseTime = WeaponState.OneUseTime
        , ReloadTime = WeaponState.ReloadTime};
        EventTire.SendEvent(new SaveWeaponStateEvent() { WeaponData = weaponData });
    }

}