using System;
using UnityEngine;
using System.Collections.Generic;

public class LocalEventTire : MonoBehaviour, IETireHierarchy
{
    private ETire Tire = new ETire();
    private IEventTire Parent;
    private HashSet<IEventTire> Children = new HashSet<IEventTire>();

    void Start()
    {
        ConnectToParentTire();
    }    

    void ConnectToParentTire()
    {
        Transform parent = transform.parent;
        while(parent != null)
        {
            var parentTire = parent.GetComponent<IETireHierarchy>();
            if(parentTire != null)
            {
                parentTire.AddChild(this);
                Parent = parentTire;
                return;
            }
            else
            {
                parent = parent.parent;
            }
        }

        Parent = GlobalEventTire.Instance;
    }

    public void SendEvent(TEPath path, TireEventType type, System.Object param)
    {
        Tire.SendEvent(type, param);
        if (path == TEPath.Local)
        {
        }
        else if(path == TEPath.Up)
        {
            Parent.SendEvent(path, type, param);
        }
        else if(path == TEPath.Down)
        {
            foreach(var child in Children)
            {
                child.SendEvent(path, type, param);
            }
        }
        else if(path == TEPath.UpDown)
        {
            Parent.SendEvent(TEPath.Up, type, param);
            foreach (var child in Children)
            {
                child.SendEvent(TEPath.Down, type, param);
            }
        }
    }

    public void AddEventListener(TireEventType type, Action<System.Object> handler)
    {
        Tire.AddEventListener(type, handler);
    }

    public void RemoveEventListener(TireEventType type, Action<System.Object> handler)
    {
        Tire.RemoveEventListener(type, handler);
    }

    public void AddChild(IEventTire child)
    {
        Children.Add(child);

    }
}