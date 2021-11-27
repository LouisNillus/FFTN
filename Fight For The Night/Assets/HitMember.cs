using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMember : MonoBehaviour
{
    public PlayerHitsManager phm;

    public bool canHit;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(phm.otherPlayerTag))
        {
            Debug.Log(other.name);
        }
    }

}
