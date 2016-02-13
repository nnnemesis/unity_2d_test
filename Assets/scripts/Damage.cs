using UnityEngine;
using System.Collections.Generic;

public enum DamageType
{
    Hit,
    Bullet,
    Explosion
}

public interface IDamageble
{
    void InflictDamage(DamageType Type, float Amount);
}