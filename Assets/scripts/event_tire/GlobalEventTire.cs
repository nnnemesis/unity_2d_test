using System;
using UnityEngine;
using System.Collections.Generic;

public class GlobalEventTire : Singleton<GlobalEventTire>, IETireHierarchy
{
    private ETire Tire = new ETire();
    private HashSet<IEventTire> Children = new HashSet<IEventTire>();

    public void SendEvent(TEPath path, TireEventType type, object param)
    {
        Tire.SendEvent(type, param);
        if (path == TEPath.Local)
        {
        }
        else if (path == TEPath.Up)
        {
        }
        else if (path == TEPath.Down)
        {
            foreach (var child in Children)
            {
                child.SendEvent(path, type, param);
            }
        }
        else if (path == TEPath.UpDown)
        {
            foreach (var child in Children)
            {
                child.SendEvent(TEPath.Down, type, param);
            }
        }
    }

    public void AddChild(IEventTire child)
    {
        Children.Add(child);
    }

    public void AddEventListener(TireEventType type, Action<object> handler)
    {
        Tire.AddEventListener(type, handler);
    }

    public void RemoveEventListener(TireEventType type, Action<object> handler)
    {
        Tire.RemoveEventListener(type, handler);
    }
}