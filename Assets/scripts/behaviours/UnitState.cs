using UnityEngine;
using System.Collections;

public class UnitState : MonoBehaviour
{

    private IEventTire EventTire;

    private GameObject _CurrentWeapon;
    public GameObject CurrentWeapon
    {
        get { return _CurrentWeapon; }
        set
        {
            if (_CurrentWeapon != value)
            {
                _CurrentWeapon = value;
                EventTire.SendEvent(new ChangedCurrentWeapon() { NewCurrentWeapon = value });
            }
        }
    }

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
    }

}