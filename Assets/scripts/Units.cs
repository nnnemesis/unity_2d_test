using UnityEngine;
using System.Collections;

public enum UnitType
{
    Player,
    Mutant,
}

public interface IUnit
{
    UnitType GetUnitType();
}