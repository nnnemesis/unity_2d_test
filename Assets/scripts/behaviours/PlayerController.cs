using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour, ITireEventListener {

    private PlayerState PlayerState;
    private IEventTire EventTire;
    public Transform LeftHandTransform;
    public GameObject InitWeapon;   // test

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        PlayerState = GetComponent<PlayerState>();
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
    }

    void Start() {
        GameObject testWeapon = (GameObject)Instantiate(InitWeapon, Vector3.zero, Quaternion.identity);
        testWeapon.transform.SetParent(LeftHandTransform, false);
        PlayerState.CurrentWeapon = testWeapon.GetComponent<IWeapon>();
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
        if(PlayerState.CurrentWeapon != null)
        {
            if (actions[ControlAction.MainAttack])
            {
                PlayerState.CurrentWeapon.StartUsing();
            }
            else
            {
                PlayerState.CurrentWeapon.StopUsing();
            }
        }
    }
}
