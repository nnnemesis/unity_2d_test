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
        var ev = new LoadWeaponStateEvent() { SavedWeaponState = new SavedWeaponState() { WeaponType = WeaponState.WeaponType } };
        EventTire.SendEvent(ev);
        var savedState = ev.SavedWeaponState;
        if (savedState != null)
        {
            WeaponState.CurrentMagazineAmmo = savedState.CurrentMagazineAmmo;
            WeaponState.CurrentTotalAmmo = savedState.CurrentTotalAmmo;
            WeaponState.OneUseTime = savedState.OneUseTime;
            WeaponState.ReloadTime = savedState.ReloadTime;
            WeaponState.MaxMagazineAmmo = savedState.MaxMagazineAmmo;
            WeaponState.MaxTotalAmmo = savedState.MaxTotalAmmo;
            WeaponState.DamageAmount = savedState.DamageAmount;
        }
    }

    void SaveWeaponState()
    {
        var savedWeaponState = new SavedWeaponState() {
            WeaponType = WeaponState.WeaponType
        , CurrentMagazineAmmo = WeaponState.CurrentMagazineAmmo
        , CurrentTotalAmmo = WeaponState.CurrentTotalAmmo
        , DamageAmount = WeaponState.DamageAmount
        , MaxMagazineAmmo = WeaponState.MaxMagazineAmmo
        , MaxTotalAmmo = WeaponState.MaxTotalAmmo
        , OneUseTime = WeaponState.OneUseTime
        , ReloadTime = WeaponState.ReloadTime};
        EventTire.SendEvent(new SaveWeaponStateEvent() { SavedWeaponState = savedWeaponState });
    }

}