using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboInput
{

    public KeyCode key;
    public HitType hitType;
    public float comboOffsetTime;
    [HideInInspector]
    public float animationTime;
    public HitMemberName member;
    public int damages;
    public bool hasBeenPlayed;

    public string animationName = "";

    public ComboInput(ComboInput comboInput)
    {
        key = comboInput.key;
        hitType = comboInput.hitType;
        comboOffsetTime = comboInput.comboOffsetTime;
        animationTime = comboInput.animationTime;
        member = comboInput.member;
        damages = comboInput.damages;
        animationName = comboInput.animationName;
    }

    public float CastTime()
    {
        return animationTime + comboOffsetTime;
    }
}


public enum HitType {Light, Medium, Heavy}

// Player 1 :
// Y = 3 = Pied gauche
// X = 2 = Main droite
// A = 0 = Main gauche

// Player 2 :
// Y = 3 = 
// X = 2 = 
// A = 0 = 
