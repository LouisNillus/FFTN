using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combo List", menuName = "Combo List")]
public class ComboList : ScriptableObject
{

    public KeyCode low;
    public KeyCode mid;
    public KeyCode high;

    [Header("Combos List")]
    public List<Combo> combos = new List<Combo>();
}
