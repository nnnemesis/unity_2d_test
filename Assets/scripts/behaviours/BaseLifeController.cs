using UnityEngine;
using System.Collections;

public class BaseLifeController : MonoBehaviour, IDamageble
{
    private BaseLifeState BaseLifeState;

    void Awake()
    {
        BaseLifeState = GetComponent<BaseLifeState>();
    }

    public void InflictDamage(DamageType Type, float Amount)
    {
        BaseLifeState.Health -= Amount;
    }

}