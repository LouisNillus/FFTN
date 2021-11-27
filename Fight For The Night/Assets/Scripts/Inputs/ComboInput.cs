using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComboInput
{

    public KeyCode key;
    public TypeOfInput inputType;
    public float castTime;

    public ComboInput(KeyCode key, TypeOfInput inputType, float castTime = 0f)
    {
        this.key = key;
        this.inputType = inputType;
        this.castTime = castTime;
    }
}


public enum TypeOfInput {Movement, Hit}
