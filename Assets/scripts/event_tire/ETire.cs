using System;
using System.Collections.Generic;

public class ETire // Base event tire
{
    private Dictionary<TireEventType, HashSet<Action<System.Object>>> HandlersMap = new Dictionary<TireEventType, HashSet<Action<System.Object>>>();

    public void SendEvent(TireEventType type, Object param)
    {
        var handlersList = HandlersMap[type];
        if (handlersList == null)
        {
            foreach (var handler in handlersList)
            {
                handler(param);
            }
        }
    }

    public void AddEventListener(TireEventType type, Action<object> handler)
    {
        var handlersList = HandlersMap[type];
        if(handlersList == null)
        {
            handlersList = new HashSet<Action<System.Object>>();
            HandlersMap[type] = handlersList;
        }
        handlersList.Add(handler);
    }

    public void RemoveEventListener(TireEventType type, Action<object> handler)
    {
        var handlersList = HandlersMap[type];
        if (handlersList == null)
        {
            handlersList.Remove(handler);
        }
    }

}

