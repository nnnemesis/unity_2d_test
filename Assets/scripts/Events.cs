using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TireEventType
{
    ControlEvent,
    ChangedDirectionEvent,
    ChangedMoveStateEvent,
    ChangedJumpStateEvent,
    WeaponUseStateChangedEvent,
    WeaponUseStateDoneEvent,
    ChangedCurrentWeapon,
    ChangedHealthEvent,
    ChangedAiTarget,
}

public abstract class TireEvent
{
    public TireEventType Type;
}

public interface ITireEventListener
{
    void OnTireEvent(TireEvent ev);
}

public interface IHasEventTire
{
    IEventTire GetTire();
}

public interface IEventTire
{
    void SendEvent(TireEvent ev);
    void AddEventListener(TireEventType type, ITireEventListener listener);
    void RemoveEventListener(TireEventType type, ITireEventListener listener);
}

public class EventTire : IEventTire
{
    private Dictionary<TireEventType, List<ITireEventListener>> listeners = new Dictionary<TireEventType, List<ITireEventListener>>();

    public void SendEvent(TireEvent ev)
    {
        List<ITireEventListener> list;
        if (listeners.TryGetValue(ev.Type, out list))
        {
            foreach (ITireEventListener listener in list)
            {
                listener.OnTireEvent(ev);
            }
        }
    }

    public void AddEventListener(TireEventType type, ITireEventListener listener)
    {
        List<ITireEventListener> list;
        if (listeners.TryGetValue(type, out list))
        {
            list.Add(listener);
        }
        else
        {
            List<ITireEventListener> list1 = new List<ITireEventListener>();
            list1.Add(listener);
            listeners.Add(type, list1);
        }
    }

    public void RemoveEventListener(TireEventType type, ITireEventListener listener)
    {
        List<ITireEventListener> list;
        if (listeners.TryGetValue(type, out list))
        {
            list.Remove(listener);
        }
    }
}

public class GlobalTire : IEventTire
{

    private GlobalTire() { }

    private static IEventTire Instance;
    static GlobalTire()
    {
        Instance = new EventTire();
    }

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
