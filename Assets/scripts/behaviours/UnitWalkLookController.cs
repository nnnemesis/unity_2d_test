using UnityEngine;
using System.Collections.Generic;

public class UnitWalkLookController : MonoBehaviour, ITireEventListener {

    private BaseMoveState BaseMoveState;
    private IEventTire EventTire;

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
        BaseMoveState = GetComponent<BaseMoveState>();
        EventTire.AddEventListener(TireEventType.ChangedMoveStateEvent, this);
        EventTire.AddEventListener(TireEventType.ChangedDirectionEvent, this);
    }

    public void OnTireEvent(TireEvent ev)
    {
        if(ev.Type == TireEventType.ChangedMoveStateEvent)
        {
            OnChangedMoveStateEvent((ChangedMoveStateEvent)ev);
        }
        else if (ev.Type == TireEventType.ChangedDirectionEvent)
        {
            OnChangedDirectionEvent((ChangedDirectionEvent)ev);
        }
    }

    void OnChangedMoveStateEvent(ChangedMoveStateEvent e)
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

    void OnChangedDirectionEvent(ChangedDirectionEvent e)
    {
        var rotation = transform.rotation;
        if (e.NewDirection > 0)
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
