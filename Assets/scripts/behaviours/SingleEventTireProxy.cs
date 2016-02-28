using UnityEngine;
using System.Collections;

public class SingleEventTireProxy : MonoBehaviour, IEventTire
{

    public IEventTire Instance;
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