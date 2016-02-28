using UnityEngine;
using System.Collections;

public class BaseLifeState : MonoBehaviour
{
    private IEventTire EventTire;

    private float _Health = 100f;
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
                EventTire.SendEvent(new ChangedHealthEvent() { NewHealth = value });
            }
        }
    }

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
    }
    
}