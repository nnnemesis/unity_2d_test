using System;
using System.Collections.Generic;

public enum TEType  // Tire event type
{

}

public enum TEPath   // Event path
{
    Local,
    Up,
    Down,
    UpDown
}

public interface IETire // I Event tire
{
    void SendEvent(TEPath path, TEType type, Object param);
    void AddEventListener(TEType type, Action<Object> handler);
    void RemoveEventListener(TEType type, Action<Object> handler);
}

public interface IETireHierarchy : IETire
{
    void AddChild(IETire child);
}

