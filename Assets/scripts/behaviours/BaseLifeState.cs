using UnityEngine;
using System.Collections;

public class BaseLifeState : MonoBehaviour
{
    private IEventTire EventTire;

    public float _Health = 100f;
    public float Health
    {
        get
        {
            return _Health;
        }
        set
        {
            if (_Health != value)
            {
                _Health = value;
                EventTire.SendEvent(TEPath.Up, TireEventType.ChangedHealthEvent, value);
            }
        }
    }

    void Start()
    {
        EventTire = this.GetEventTire();
    }
    
}