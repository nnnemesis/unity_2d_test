using UnityEngine;
using System.Collections.Generic;
using System;

public enum PrefabIdentifier
{
    None,
    WeaponAxe,
    WeaponStick,
    WeaponPistol,
    WeaponAutomat
}

public class PrefabStore : MonoBehaviour
{

    [Serializable]
    public class Pair
    {
        public PrefabIdentifier Identifier;
        public GameObject Prefab;
    }

    public static PrefabStore Instance;

    public List<Pair> PrefabsList = new List<Pair>();
    private Dictionary<PrefabIdentifier, GameObject> PrefabsMap = new Dictionary<PrefabIdentifier, GameObject>();

    void Awake()
    {
        Instance = this;
        foreach(var pair in PrefabsList)
        {
            PrefabsMap[pair.Identifier] = pair.Prefab;
        }
    }   

    public GameObject GetPrefab(PrefabIdentifier identifier)
    {
        Debug.Log("GetPrefab "+identifier);
        return PrefabsMap[identifier];
    }
    
}