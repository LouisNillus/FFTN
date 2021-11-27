using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Rules", menuName = "Game Rules Profile")]
public class GameRules : ScriptableObject
{
    public int gameDuration;
    public int roundsCount;
}
