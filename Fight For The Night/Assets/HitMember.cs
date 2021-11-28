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
            if(phm.buffer.currentInput != null)
            {
                other.GetComponent<BodyPart>().associatedPlayer.TakeDamages(phm.buffer.currentInput.damages);
                other.GetComponent<BodyPart>().PlayAnimation();

                switch (phm.buffer.currentInput.hitType)
                {
                    case HitType.Light:
                        Instantiate(FightManager.instance.light, this.transform.position, Quaternion.identity);
                        break;
                    case HitType.Medium:
                        Instantiate(FightManager.instance.medium, this.transform.position, Quaternion.identity);
                        break;
                    case HitType.Heavy:
                        Instantiate(FightManager.instance.heavy, this.transform.position, Quaternion.identity);
                        break;
                }

                Debug.Log(other.name);
                canHit = false;
            }
        }
    }



    public void Enable() => canHit = true;
    public void Disable() => canHit = false;

}
