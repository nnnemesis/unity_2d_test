using UnityEngine;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    private EnemyAiState EnemyAiState;
    private IEventTire EventTire;
    private Dictionary<ControlAction, bool> Actions = new Dictionary<ControlAction, bool>();

    void Awake()
    {
        EnemyAiState = GetComponent<EnemyAiState>();
        EventTire = GetComponent<EventTire>();
    }

    void FixedUpdate()
    {
        if(EnemyAiState.CurrentTarget == null)
        {
            //find new target
            Physics2D.CircleCast(transform.position, EnemyAiState.FindTargetDistance, Vector2.zero);

        }
        else
        {
            var targetTransform = EnemyAiState.CurrentTarget.transform;
            float targetDistance = Vector2.Distance(targetTransform.position, transform.position);
            Actions.Clear();
            if (targetDistance > EnemyAiState.AttackTargetDistance)
            {
                // move to enemy
                if((targetTransform.position.x - transform.position.x) >= 0)
                {
                    // move forward
                    Actions[ControlAction.WalkForward] = true;
                }
                else
                {
                    // move backward
                    Actions[ControlAction.WalkBack] = true;
                }
            }
            else
            {
                // attack enemy
                Actions[ControlAction.MainAttack] = true;
            }
            EventTire.SendEvent(new ControlEvent() { Actions = Actions });
        }
    }



}