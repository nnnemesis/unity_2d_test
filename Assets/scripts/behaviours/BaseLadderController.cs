using UnityEngine;
using System.Collections;

public class BaseLadderController : MonoBehaviour, ILadder
{

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.LogWarning("OnTriggerEnter");
        if (other.gameObject.CompareTag("Player"))
        {

        }
    }

}