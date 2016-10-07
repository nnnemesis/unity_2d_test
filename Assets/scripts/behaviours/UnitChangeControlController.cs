using UnityEngine;
using System.Collections.Generic;

public class UnitChangeControlController : MonoBehaviour {

    private BaseMoveState BaseMoveState;
    private IEventTire EventTire;

    void Start()
    {
        EventTire = this.GetEventTire();
        BaseMoveState = GetComponent<BaseMoveState>();
        EventTire.AddEventListener(TireEventType.ControlEvent, OnControlEvent);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.LogWarning("OnTriggerEnter");
        if (other.GetComponent<Ladder>() && BaseMoveState.MoveControlType != UnitMoveControlType.LadderControl)
        {
            BaseMoveState.CanUseLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.LogWarning("OnTriggerEnter");
        if (other.GetComponent<Ladder>() && BaseMoveState.MoveControlType == UnitMoveControlType.LadderControl)
        {
            BaseMoveState.MoveControlType = UnitMoveControlType.GroundControl;
            BaseMoveState.CanUseLadder = false;
            Destroy(GetComponent<LadderMoveController>());
            gameObject.AddComponent<BaseMoveController>();
        }
    }

    private void OnControlEvent(object param)
    {
        Dictionary<ControlAction, bool> actions = (Dictionary<ControlAction, bool>)param;
        if(BaseMoveState.CanUseLadder && actions[ControlAction.Use])
        {
            BaseMoveState.MoveControlType = UnitMoveControlType.LadderControl;
            BaseMoveState.CanUseLadder = false;
            Destroy(GetComponent<BaseMoveController>());
            gameObject.AddComponent<LadderMoveController>();
        }
    }
}
