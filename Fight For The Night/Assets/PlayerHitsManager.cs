using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitsManager : MonoBehaviour
{
    public HitMember leftHand;
    public HitMember rightHand;

    public HitMember leftFoot;
    public HitMember rightFoot;

    public Buffer buffer;

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

    public HitMember GetHitMemberFromType(HitMemberName memberName)
    {
        switch (memberName)
        {
            case HitMemberName.LeftHand:
                return leftHand;
            case HitMemberName.RightHand:
                return rightHand;
            case HitMemberName.LeftFoot:
                return leftFoot;
            case HitMemberName.RightFoot:
                return rightFoot;
        }

        return null;
    }
}

public enum HitMemberName {LeftHand, RightHand, LeftFoot, RightFoot}
