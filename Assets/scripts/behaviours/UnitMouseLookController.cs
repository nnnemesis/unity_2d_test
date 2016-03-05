using UnityEngine;
using System.Collections.Generic;

public class UnitMouseLookController : MonoBehaviour, ITireEventListener {

    private BaseMoveState BaseMoveState;
    private IEventTire EventTire;

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
        BaseMoveState = GetComponent<BaseMoveState>();
        EventTire.AddEventListener(TireEventType.ChangedDirectionEvent, this);
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

    public void OnTireEvent(TireEvent ev)
    {
        if(ev.Type == TireEventType.ChangedDirectionEvent)
        {
            OnChangedDirectionEvent((ChangedDirectionEvent)ev);
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
