using UnityEngine;
using System.Collections;

public class ChangedAiTarget : TireEvent
{
    public ChangedAiTarget() { Type = TireEventType.ChangedAiTarget; }
    public GameObject NewTarget;

    public override string ToString()
    {
        return "Type " + Type + " NewTarget " + NewTarget.name;
    }
}

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
                EventTire.SendEvent(new ChangedAiTarget() { NewTarget = value });
            }
        }
    }

    void Start()
    {
        EventTire = GetComponent<IEventTire>();
    }

}