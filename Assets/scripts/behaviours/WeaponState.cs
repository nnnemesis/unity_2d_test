using UnityEngine;
using System.Collections;

public class WeaponState : MonoBehaviour
{
    private IEventTire EventTire;
    public WeaponType WeaponType;

    public int _CurrentMagazineAmmo = 0;
    public int CurrentMagazineAmmo
    {
        get { return _CurrentMagazineAmmo; }
        set
        {
            if (_CurrentMagazineAmmo != value)
            {
                _CurrentMagazineAmmo = value;
                EventTire.SendEvent(new WeaponCurrentAmmoChangedEvent() { NewState = value });
            }
        }
    }

    public int _CurrentTotalAmmo = 0;
    public int CurrentTotalAmmo
    {
        get { return _CurrentTotalAmmo; }
        set
        {
            if (_CurrentTotalAmmo != value)
            {
                _CurrentTotalAmmo = value;
                EventTire.SendEvent(new WeaponCurrentTotalAmmoChangedEvent() { NewState = value });
            }
        }
    }

    public int MaxTotalAmmo = 0;
    public int MaxMagazineAmmo = 0;
    public float ReloadTime = 0;
    public float OneUseTime = 0.5f;   // sec
    public float DamageAmount = 20f;

    private WeaponUseState _UseState = WeaponUseState.Idle;
    public WeaponUseState UseState
    {
        get { return _UseState; }
        set
        {
            if(_UseState != value)
            {
                _UseState = value;
                EventTire.SendEvent(new WeaponUseStateChangedEvent() { NewState = value });
            }
        }
    }

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
    }
    
}