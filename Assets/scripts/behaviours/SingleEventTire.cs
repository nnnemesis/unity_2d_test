using UnityEngine;
using System.Collections;

public class SingleEventTire : MonoBehaviour, IEventTire
{
    private IEventTire Instance = new EventTire();

    public void SendEvent(TireEvent ev)
    {
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