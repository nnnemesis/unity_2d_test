using UnityEngine;
using System.Collections;

public class BaseLifeController : MonoBehaviour, IDamageble
{
    private BaseLifeState BaseLifeState;

    void Start()
    {
        BaseLifeState = GetComponent<BaseLifeState>();
    }

    public void InflictDamage(DamageType Type, float Amount)
    {
        BaseLifeState.Health -= Amount;
        if(BaseLifeState.Health <= 0)
        {
            DestroyOnHealthEnd();
        }
    }

    void DestroyOnHealthEnd()
    {
        Destroy(gameObject);
    }

}