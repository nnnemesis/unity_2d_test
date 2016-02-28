using UnityEngine;
using System.Collections.Generic;

public class UnitChangeControlController : MonoBehaviour, ITireEventListener {

    private BaseMoveState BaseMoveState;
    private IEventTire EventTire;

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
        BaseMoveState = GetComponent<BaseMoveState>();
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
    }

    public void OnTireEvent(TireEvent ev)
    {
        if(ev.Type == TireEventType.ControlEvent)
        {
            OnControlEvent((ControlEvent)ev);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.LogWarning("OnTriggerEnter");
        if (other.CompareTag("Ladder") && BaseMoveState.MoveControlType != UnitMoveControlType.LadderControl)
        {
            BaseMoveState.CanUseLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.LogWarning("OnTriggerEnter");
        if (other.CompareTag("Ladder") && BaseMoveState.MoveControlType == UnitMoveControlType.LadderControl)
        {
            BaseMoveState.MoveControlType = UnitMoveControlType.GroundControl;
            BaseMoveState.CanUseLadder = false;
            Destroy(GetComponent<LadderMoveController>());
            gameObject.AddComponent<BaseMoveController>();
        }
    }

    private void OnControlEvent(ControlEvent e)
    {
        Dictionary<ControlAction, bool> actions = e.Actions;
        if(BaseMoveState.CanUseLadder && actions[ControlAction.Use])
        {
            BaseMoveState.MoveControlType = UnitMoveControlType.LadderControl;
            BaseMoveState.CanUseLadder = false;
            Destroy(GetComponent<BaseMoveController>());
            gameObject.AddComponent<LadderMoveController>();
        }
    }
}
