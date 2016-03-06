using UnityEngine;
using System.Collections.Generic;

public class KeyBinder : MonoBehaviour {

    private Dictionary<ControlAction, bool> State = new Dictionary<ControlAction, bool>();
    private Dictionary<KeyCode, ControlAction> KeyMap = new Dictionary<KeyCode, ControlAction>();

    private IEventTire EventTire;

    void Start()
    {
        EventTire = GetComponent<IEventTire>();

        KeyMap.Add(KeyCode.D, ControlAction.WalkForward);
        KeyMap.Add(KeyCode.A, ControlAction.WalkBack);
        KeyMap.Add(KeyCode.Space, ControlAction.Jump);
        KeyMap.Add(KeyCode.LeftShift, ControlAction.Shift);
        KeyMap.Add(KeyCode.RightShift, ControlAction.Shift);
        KeyMap.Add(KeyCode.E, ControlAction.Use);
        KeyMap.Add(KeyCode.W, ControlAction.WalkUp);
        KeyMap.Add(KeyCode.S, ControlAction.WalkDown);
        KeyMap.Add(KeyCode.R, ControlAction.Recharge);
        KeyMap.Add(KeyCode.LeftControl, ControlAction.SitDown);
        KeyMap.Add(KeyCode.RightControl, ControlAction.SitDown);

        KeyMap.Add(KeyCode.Mouse0, ControlAction.MainAttack);

        foreach (var pair in KeyMap)
        {
            State[pair.Value] = false;
        }

        State[ControlAction.NextWeapon] = false;
        State[ControlAction.PrevWeapon] = false;

        EventTire.SendEvent(new ControlEvent() { Actions = State });
    }

    void Update()
    {
        bool changed = false;
        //Debug.Log("Checking keys");
        foreach (var pair in KeyMap)
        {
            //Debug.Log("Check key "+ pair.Key);
            if (Input.GetKeyDown(pair.Key))
            {
                State[pair.Value] = true;
                changed = true;
            }
            else if (Input.GetKeyUp(pair.Key))
            {
                State[pair.Value] = false;
                changed = true;
            }
        }

        float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        State[ControlAction.PrevWeapon] = false;
        State[ControlAction.NextWeapon] = false;
        if (mouseScrollWheel > 0)
        {
            State[ControlAction.NextWeapon] = true;
            changed = true;
        }
        else if (mouseScrollWheel < 0)
        {
            State[ControlAction.PrevWeapon] = true;
            changed = true;
        }

        if (changed)
        {
            //Debug.Log("ControlEvent");
            EventTire.SendEvent(new ControlEvent() { Actions = State });
        }
    }

}
