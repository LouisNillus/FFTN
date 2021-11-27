using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager instance;

    public GameRules rules;


    private void Awake()
    {
        instance = this;
    }



}
