using UnityEngine;
using System.Collections;

public class WeaponState : MonoBehaviour
{
    private IEventTire EventTire;
    public WeaponType Type;
    
    public int CurrentMagazineAmmo = 0;
    public int CurrentTotalAmmo = 0;
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

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
    }
    
}