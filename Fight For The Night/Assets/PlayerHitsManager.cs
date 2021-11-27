using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitsManager : MonoBehaviour
{
    public HitMember leftHand;
    public HitMember rightHand;

    public HitMember leftFoot;
    public HitMember rightFoot;

    public string thisPlayerTag;
    public string otherPlayerTag;
    public List<GameObject> bodyParts = new List<GameObject>();


    // Start
    void Start()
    {
        foreach (GameObject go in bodyParts) go.tag = thisPlayerTag;
    }

    // Update
    void Update()
    {
        
    }
}
