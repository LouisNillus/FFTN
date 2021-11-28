using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HitMember : MonoBehaviour
{
    public PlayerHitsManager phm;

    public bool canHit;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(phm.otherPlayerTag) && canHit)
        {
            if(phm.buffer.lastInput != null)
            {
                other.GetComponent<BodyPart>().associatedPlayer.TakeDamages(phm.buffer.lastInput.damages);
                Debug.Log(other.name);
                canHit = false;
            }
        }
    }



    public void Enable() => canHit = true;
    public void Disable() => canHit = false;   
    
}
