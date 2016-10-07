using UnityEngine;
using System.Collections.Generic;

public class SitDownController : MonoBehaviour {

    private BaseMoveState BaseState;
    private IEventTire EventTire;
    public BoxCollider2D UpperCollider;

    void Start()
    {
        EventTire = this.GetEventTire();
        BaseState = GetComponent<BaseMoveState>();
        UpperCollider.enabled = true;

        EventTire.AddEventListener(TireEventType.ControlEvent, OnControlEvent);
        EventTire.AddEventListener(TireEventType.ChangedSitDownEvent, OnChangedSitDownEvent);
    }

    void OnDestroy()
    {
        EventTire.RemoveEventListener(TireEventType.ControlEvent, OnControlEvent);
        EventTire.RemoveEventListener(TireEventType.ChangedSitDownEvent, OnChangedSitDownEvent);
    }

    void OnChangedSitDownEvent(object param)
    {
        if (BaseState.SitDown)
        {
            UpperCollider.enabled = false;
        }
        else
        {
            UpperCollider.enabled = true;
        }
    }

    private void OnControlEvent(object param)
    {
        Dictionary<ControlAction, bool> actions = (Dictionary < ControlAction, bool>)param;
        if(actions[ControlAction.SitDown])
        {
            BaseState.SitDown = true;
        }
        else
        {
            BaseState.SitDown = false;
        }
    }
}
