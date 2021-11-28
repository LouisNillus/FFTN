using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboInput
{

    public KeyCode key;
    public TypeOfInput inputType;
    public float castTime;
    public HitMemberName member;
    public int damages;

    public ComboInput(ComboInput comboInput)
    {
        key = comboInput.key;
        inputType = comboInput.inputType;
        castTime = comboInput.castTime;
        member = comboInput.member;
        damages = comboInput.damages;
    }
}


public enum TypeOfInput {Hit, Movement}

// Player 1 :
// Y = 3 = Pied gauche
// X = 2 = Main droite
// A = 0 = Main gauche

// Player 2 :
// Y = 3 = 
// X = 2 = 
// A = 0 = 
