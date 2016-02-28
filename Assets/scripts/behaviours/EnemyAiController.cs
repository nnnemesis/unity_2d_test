using UnityEngine;
using System.Collections.Generic;

public class EnemyAiController : MonoBehaviour
{
    private EnemyAiState EnemyAiState;
    private IEventTire EventTire;
    private Dictionary<ControlAction, bool> Actions = new Dictionary<ControlAction, bool>();
    private IUnit SelfUnit;

    void Start()
    {
        EnemyAiState = GetComponent<EnemyAiState>();
        EventTire = GetComponent<IEventTire>();
        SelfUnit = GetComponent<IUnit>();

        Actions.Add(ControlAction.WalkForward, false);
        Actions.Add(ControlAction.WalkBack, false);
        Actions.Add(ControlAction.Jump, false);
        Actions.Add(ControlAction.Shift, false);
        Actions.Add(ControlAction.MainAttack, false);
        Actions.Add(ControlAction.Use, false);
        Actions.Add(ControlAction.WalkUp, false);
        Actions.Add(ControlAction.WalkDown, false);
        Actions.Add(ControlAction.Recharge, false);
        Actions.Add(ControlAction.NextWeapon, false);
        Actions.Add(ControlAction.PrevWeapon, false);
    }

    void FixedUpdate()
    {
        if(EnemyAiState.CurrentTarget == null)
        {
            //find new target
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, EnemyAiState.FindTargetDistance, Vector2.zero);
            foreach(var hit in hits)
            {
                var hitedObject = hit.transform.gameObject;
                if(GetRelationType(hitedObject) == RelationType.Enemy)
                {
                    //Debug.Log("New target found " + hitedObject.gameObject.name);
                    EnemyAiState.CurrentTarget = hitedObject;
                    break;
                }
            }

        }
        else
        {
            var targetTransform = EnemyAiState.CurrentTarget.transform;
            float targetDistance = Vector2.Distance(targetTransform.position, transform.position);
            bool changed = false;
            if (targetDistance > EnemyAiState.AttackTargetDistance)
            {
                // move to enemy
                if ((targetTransform.position.x - transform.position.x) >= 0)
                {
                    // move forward
                    SetAction(ref changed, ControlAction.WalkForward, true);
                    SetAction(ref changed, ControlAction.WalkBack, false);
                    SetAction(ref changed, ControlAction.MainAttack, false);
                }
                else
                {
                    // move backward
                    SetAction(ref changed, ControlAction.WalkForward, false);
                    SetAction(ref changed, ControlAction.WalkBack, true);
                    SetAction(ref changed, ControlAction.MainAttack, false);
                }
            }
            else
            {
                // attack enemy
                SetAction(ref changed, ControlAction.WalkForward, false);
                SetAction(ref changed, ControlAction.WalkBack, false);
                SetAction(ref changed, ControlAction.MainAttack, true);
            }
            if (changed)
            {
                //Debug.Log("AI actions");
                //foreach (var pair in Actions)
                //{
                //    Debug.Log("AI action "+ pair.Key + " value "+pair.Value);
                //}
                EventTire.SendEvent(new ControlEvent() { Actions = Actions });
            }                
        }
    }

    void SetAction(ref bool prevAction, ControlAction action, bool newState)
    {
        if(Actions[action] != newState)
        {
            Actions[action] = newState;
            prevAction |= true;
        }
        else
        {
            prevAction |= false;
        }
    }

    RelationType GetRelationType(GameObject gameObject)
    {
        var unit = gameObject.GetComponent<IUnit>();
        if (unit != null)
        {
            if(unit.GetUnitType() == SelfUnit.GetUnitType())
            {
                return RelationType.Friend;
            }
            else
            {
                return RelationType.Enemy;
            }
        }
        else
        {
            return RelationType.Neutral;
        }
    }

}