using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HitMember : MonoBehaviour
{
    public PlayerHitsManager phm;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(phm.otherPlayerTag))
        {
            Debug.Log(other.name);
        }
    }

}
