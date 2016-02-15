using UnityEngine;
using System.Collections.Generic;

public enum RelationType
{
    Enemy,
    Neutral,
    Friend
}

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