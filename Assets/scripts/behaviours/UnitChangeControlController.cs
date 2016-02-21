using UnityEngine;
using System.Collections.Generic;

public class UnitChangeControlController : MonoBehaviour, ITireEventListener {

    private BaseMoveState BaseMoveState;
    private IEventTire EventTire;
    private BaseMoveController BaseMoveController;
    private LadderMoveController LadderMoveController;

    void Awake()
    {
        EventTire = GetComponent<IEventTire>();
        BaseMoveState = GetComponent<BaseMoveState>();
        EventTire.AddEventListener(TireEventType.ControlEvent, this);
        BaseMoveController = GetComponent<BaseMoveController>();
        LadderMoveController = GetComponent<LadderMoveController>();
        LadderMoveController.enabled = false;
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
            BaseMoveController.enabled = true;
            LadderMoveController.enabled = false;
        }
    }

    private void OnControlEvent(ControlEvent e)
    {
        Dictionary<ControlAction, bool> actions = e.Actions;
        if(BaseMoveState.CanUseLadder && actions[ControlAction.Use])
        {
            BaseMoveState.MoveControlType = UnitMoveControlType.LadderControl;
            BaseMoveState.CanUseLadder = false;
            BaseMoveController.enabled = false;
            LadderMoveController.enabled = true;
        }
    }
}
