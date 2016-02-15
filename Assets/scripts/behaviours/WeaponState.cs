using UnityEngine;
using System.Collections;

public class WeaponState : MonoBehaviour
{
    private IEventTire EventTire;
    public Transform Transform;
    public Rigidbody2D Rigidbody;
    public WeaponType Type;
    
    public int CurrentMagazineAmmo = 0;
    public int CurrentTotalAmmo = 0;
    public int MaxAmmo = 0;
    public float ReloadTime = 0;
    public float OneUseTime = 0.5f;   // sec
    public float DamageAmount = 20f;

    public WeaponUseState _UseState = WeaponUseState.Idle;
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

    public bool _UseStateDone = true;
    public bool UseStateDone
    {
        get { return _UseStateDone; }
        set
        {
            if(_UseStateDone != value)
            {
                _UseStateDone = value;
                EventTire.SendEvent(new WeaponUseStateDoneEvent() { WeaponState = this });
            }
        }
    }

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Transform = transform;
    }
    
}