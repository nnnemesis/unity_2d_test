using System;
using System.Collections.Generic;

public class ETire // Base event tire
{
    private Dictionary<TEType, HashSet<Action<System.Object>>> HandlersMap = new Dictionary<TEType, HashSet<Action<System.Object>>>();

    public void SendEvent(TEType type, Object param)
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

    public void AddEventListener(TEType type, Action<object> handler)
    {
        var handlersList = HandlersMap[type];
        if(handlersList == null)
        {
            handlersList = new HashSet<Action<System.Object>>();
            HandlersMap[type] = handlersList;
        }
        handlersList.Add(handler);
    }

    public void RemoveEventListener(TEType type, Action<object> handler)
    {
        var handlersList = HandlersMap[type];
        if (handlersList == null)
        {
            handlersList.Remove(handler);
        }
    }

}

