using UnityEngine;
using System.Collections;

public class EnemyAiState : MonoBehaviour
{
    public float FindTargetDistance = 20f;
    public float AttackTargetDistance = 5f;

    public IEventTire EventTire;

    private GameObject _CurrentTarget;
    public GameObject CurrentTarget
    {
        get { return _CurrentTarget; }
        set
        {
            if(_CurrentTarget != value)
            {
                _CurrentTarget = value;
                EventTire.SendEvent(TEPath.Up, TireEventType.ChangedAiTarget, value);
            }
        }
    }

    void Start()
    {
        EventTire = this.GetEventTire();
    }

}