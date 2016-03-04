using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class SomeCards<CardEnum, Card> : MonoBehaviour
{

    public static SomeCards<CardEnum, Card> Instance;

    public List<CardEnum> ListKeys = new List<CardEnum>();
    public List<Card> ListValues = new List<Card>();
    private Dictionary<CardEnum, Card> Map = new Dictionary<CardEnum, Card>();

    void Start()
    {
        Instance = this;
        for(int i = 0; i < ListKeys.Count; i+=1)
        {
            Map[ListKeys[i]] = ListValues[i];
        }
    }   

    public static Card GetCard(CardEnum key)
    {
        return Instance.Map[key];
    }
    
}