using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " New Input Profile", menuName = "Input profile")]
public class InputProfile : ScriptableObject
{
    public string horizontalAxis;
    public string verticalAxis;
    public KeyCode lightAttack;
    public KeyCode mediumAttack;
    public KeyCode heavyAttack;
    public KeyCode start;
}
