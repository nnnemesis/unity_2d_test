using UnityEngine;
using System.Collections.Generic;

public class KeyBinder : MonoBehaviour {

    private Dictionary<ControlAction, bool> State = new Dictionary<ControlAction, bool>();
    private Dictionary<KeyCode, ControlAction> KeyMap = new Dictionary<KeyCode, ControlAction>();

    private IEventTire EventTire;

    void Awake()
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

        KeyMap.Add(KeyCode.Mouse0, ControlAction.MainAttack);

        foreach (var pair in KeyMap)
        {
            State[pair.Value] = false;
        }
    }

    void Start () {
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
        if(changed)
        {
            //Debug.Log("ControlEvent");
            EventTire.SendEvent(new ControlEvent() { Actions = State });
        }
    }

}
