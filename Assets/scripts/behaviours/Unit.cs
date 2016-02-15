using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour, IUnit
{
    public UnitType UnitType;

    public UnitType GetUnitType()
    {
        return UnitType;
    }
    
}