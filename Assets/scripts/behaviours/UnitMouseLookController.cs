using UnityEngine;
using System.Collections.Generic;

public class UnitMouseLookController : MonoBehaviour {

    private BaseMoveState BaseMoveState;
    private IEventTire EventTire;

    void Start()
    {
        EventTire = this.GetEventTire();
        BaseMoveState = GetComponent<BaseMoveState>();
        EventTire.AddEventListener(TireEventType.ChangedDirectionEvent, OnChangedDirectionEvent);
    }

    void Update()
    {
        var camera = Camera.main;
        var screenMousePosition = Input.mousePosition;
        var worldMousePosition = camera.ScreenToWorldPoint(screenMousePosition);
        worldMousePosition.z = 0;
        if(worldMousePosition.x - transform.position.x >= 0)
        {
            BaseMoveState.Direction = 1;
        }
        else
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
