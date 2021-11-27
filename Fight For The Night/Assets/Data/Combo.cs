using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Combo
{
    [Header("         Combo Settings")]
    public string comboName;
    public List<ComboInput> inputs = new List<ComboInput>();
    public string animationName;
    public int damages;
    public HitMemberName member;

    public string CompileToString()
    {
        string result = "";

        foreach(ComboInput ci in inputs)
        {
            result += ci.key.ToString();
        }

        return result;
    }

}
