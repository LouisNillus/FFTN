using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combo List", menuName = "Combo List")]
public class ComboList : ScriptableObject
{
    public List<Combo> combos = new List<Combo>();
}
