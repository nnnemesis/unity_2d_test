using UnityEngine;

public static class TireUtils
{

    public static IETire GetEventTire(this MonoBehaviour behaviour)
    {
        IETire res = behaviour.GetComponent<IETire>();
        if (res != null)
            return res;

        Transform parent = behaviour.transform.parent;
        while(parent != null)
        {
            var parentTire = parent.GetComponent<IETire>();
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
