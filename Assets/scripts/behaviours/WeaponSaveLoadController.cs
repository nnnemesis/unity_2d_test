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
        EventTire = this.GetEventTire();
        LoadWeaponState();
    }

    void OnDestroy()
    {
        SaveWeaponState();
    }

    void LoadWeaponState()
    {
        var _params = new object[] { WeaponState.WeaponType, null };
        EventTire.SendEvent(TEPath.Up, TireEventType.LoadWeaponStateEvent, _params);
        var weaponData = (WeaponData)_params[1];
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
        EventTire.SendEvent(TEPath.Up, TireEventType.SaveWeaponStateEvent, weaponData);
    }

}