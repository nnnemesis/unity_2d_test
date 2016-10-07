using System;
using UnityEngine;
using System.Collections.Generic;

public class LocalEventTire : MonoBehaviour, IETireHierarchy
{
    private ETire Tire = new ETire();
    private IETire Parent;
    private HashSet<IETire> Children = new HashSet<IETire>();

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

    public void SendEvent(TEPath path, TEType type, System.Object param)
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

    public void AddEventListener(TEType type, Action<System.Object> handler)
    {
        Tire.AddEventListener(type, handler);
    }

    public void RemoveEventListener(TEType type, Action<System.Object> handler)
    {
        Tire.RemoveEventListener(type, handler);
    }

    public void AddChild(IETire child)
    {
        Children.Add(child);

    }
}