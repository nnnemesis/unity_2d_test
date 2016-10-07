using UnityEngine;
using System.Collections.Generic;

public class UnitWalkLookController : MonoBehaviour {

    private BaseMoveState BaseMoveState;
    private IEventTire EventTire;

    void Start()
    {
        EventTire = this.GetEventTire();
        BaseMoveState = GetComponent<BaseMoveState>();
        EventTire.AddEventListener(TireEventType.ChangedMoveStateEvent, OnChangedMoveStateEvent);
        EventTire.AddEventListener(TireEventType.ChangedDirectionEvent, OnChangedDirectionEvent);
    }

    void OnChangedMoveStateEvent(object param)
    {
        var moveState = BaseMoveState.MoveState;
        if (moveState == MoveState.Right)
        {
            BaseMoveState.Direction = 1;
        }
        else if(moveState == MoveState.Left)
        {
            BaseMoveState.Direction = -1;
        }
    }

    void OnChangedDirectionEvent(object param)
    {
        sbyte newDirection = (sbyte)param;
        var rotation = transform.rotation;
        if (newDirection > 0)
        {
            rotation.SetLookRotation(Vector3.forward);
        }
        else
        {
            rotation.SetLookRotation(Vector3.back);
        }
        transform.rotation = rotation;
    }

    
    
}
