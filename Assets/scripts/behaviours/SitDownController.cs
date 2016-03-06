using UnityEngine;
using System.Collections.Generic;

public class SitDownController : MonoBehaviour, ITireEventListener {

    private BaseMoveState BaseState;
    private IEventTire EventTire;
    public BoxCollider2D UpperCollider;

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
        BaseState = GetComponent<BaseMoveState>();
        UpperCollider.enabled = true;

        EventTire.AddEventListener(TireEventType.ControlEvent, this);
        EventTire.AddEventListener(TireEventType.ChangedSitDownEvent, this);
    }

    void OnDestroy()
    {
        EventTire.RemoveEventListener(TireEventType.ControlEvent, this);
        EventTire.RemoveEventListener(TireEventType.ChangedSitDownEvent, this);
    }

    public void OnTireEvent(TireEvent ev)
    {
        if(ev.Type == TireEventType.ControlEvent)
        {
            OnControlEvent((ControlEvent)ev);
        }
        else if(ev.Type == TireEventType.ChangedSitDownEvent)
        {
            OnChangedSitDownEvent((ChangedSitDownEvent)ev);
        }
    }

    void OnChangedSitDownEvent(ChangedSitDownEvent e)
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

    private void OnControlEvent(ControlEvent e)
    {
        Dictionary<ControlAction, bool> actions = e.Actions;
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
