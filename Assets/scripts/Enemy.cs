using UnityEngine;
using System.Collections.Generic;

public enum EnemyType
{
    Animal,
    Human,
    Boss
}

public interface IEnemy : IDamageble
{
    EnemyType GetType();
}