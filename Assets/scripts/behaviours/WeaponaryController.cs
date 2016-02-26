using UnityEngine;
using System.Collections.Generic;

public class WeaponaryController : MonoBehaviour, ITireEventListener {

    private UnitState UnitState;
    private IEventTire EventTire;
    public Transform LeftHandTransform;
    public GameObject InitWeapon;   // test

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        UnitState = GetComponent<UnitState>();
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
    }

    void Start() {
        GameObject testWeapon = (GameObject)Instantiate(InitWeapon, Vector3.zero, Quaternion.identity);
        testWeapon.transform.SetParent(LeftHandTransform, false);
        UnitState.CurrentWeapon = testWeapon.GetComponent<IWeapon>();
    }
	
    public void OnTireEvent(TireEvent ev)
    {
        if(ev.Type == TireEventType.ControlEvent)
        {
            OnControlEvent((ControlEvent)ev);
        }
    }

    private void OnControlEvent(ControlEvent e)
    {
        Dictionary<ControlAction, bool> actions = e.Actions;
        if(UnitState.CurrentWeapon != null)
        {
            if (actions[ControlAction.Recharge])
            {
                UnitState.CurrentWeapon.Recharge();
            }
            else if (actions[ControlAction.MainAttack])
            {
                UnitState.CurrentWeapon.StartUsing();
            }
            else
            {
                UnitState.CurrentWeapon.StopUsing();
            }
        }
    }
}
