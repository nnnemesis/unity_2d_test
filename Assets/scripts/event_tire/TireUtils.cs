using UnityEngine;

public static class TireUtils
{

    public static IEventTire GetEventTire(this MonoBehaviour behaviour)
    {
        IEventTire res = behaviour.GetComponent<IEventTire>();
        if (res != null)
            return res;

        Transform parent = behaviour.transform.parent;
        while(parent != null)
        {
            var parentTire = parent.GetComponent<IEventTire>();
            if(parentTire != null)
            {
                return parentTire;
            }
            else
            {
                parent = parent.parent;
            }
        }

        return GlobalEventTire.Instance;
    }

}
