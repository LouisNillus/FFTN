using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public PlayerController associatedPlayer;
    public string animationName;
    public void PlayAnimation()
    {
        //associatedPlayer.animator.SetTrigger(animationName);
    }
}
