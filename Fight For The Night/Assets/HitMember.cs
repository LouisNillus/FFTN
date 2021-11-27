using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMember : MonoBehaviour
{
    public PlayerHitsManager phm;

    public bool canHit;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(phm.otherPlayerTag) && canHit)
        {
            Debug.Log(other.name);
            canHit = false;
        }
    }



    public void Enable() => canHit = true;
    public void Disable() => canHit = false;   
    
}
