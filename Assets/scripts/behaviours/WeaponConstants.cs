using UnityEngine;
using System.Collections.Generic;
using System;

public class WeaponConstants : MonoBehaviour
{

    [Serializable]
    public class WeaponsPair
    {
        public WeaponType Key;
        public SavedWeaponState Value;
    }

    public static WeaponConstants Instance;

    public List<WeaponsPair> List = new List<WeaponsPair>();
    private Dictionary<WeaponType, SavedWeaponState> Map = new Dictionary<WeaponType, SavedWeaponState>();

    void Start()
    {
        Instance = this;
        foreach(var pair in List)
        {
            Map[pair.Key] = pair.Value;
        }
    }   

    public SavedWeaponState GetSavedWeaponState(WeaponType key)
    {
        //Debug.Log("GetPrefab "+identifier);
        return Map[key];
    }
    
}