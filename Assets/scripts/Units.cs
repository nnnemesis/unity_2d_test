using UnityEngine;
using System.Collections;

public enum UnitMoveControlType
{
    GroundControl,
    LadderControl,
    WaterControl,
    None
}

public enum UnitType
{
    Player,
    Mutant,
}

public interface IUnit
{
    UnitType GetUnitType();
}