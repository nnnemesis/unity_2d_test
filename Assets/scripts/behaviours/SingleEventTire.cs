using UnityEngine;
using System.Collections;

public class SingleEventTire : MonoBehaviour, IEventTire
{
    private IEventTire Instance = new EventTire();
    public bool Log = false;

    public void SendEvent(TireEvent ev)
    {
        if (Log)
        {
            Debug.Log(ev.ToString(), gameObject);
        }
        Instance.SendEvent(ev);
    }

    public void AddEventListener(TireEventType type, ITireEventListener listener)
    {
        Instance.AddEventListener(type, listener);
    }

    public void RemoveEventListener(TireEventType type, ITireEventListener listener)
    {
        Instance.RemoveEventListener(type, listener);
    }


}