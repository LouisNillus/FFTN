using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightManager : MonoBehaviour
{
    public static FightManager instance;


    public GameRules rules;

    float timer = 0f;
    public TextMeshProUGUI timerText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartTimer());
    }

    public IEnumerator StartTimer()
    {
        timer = rules.gameDuration;

        while (timer > 0)
        {
            timerText.text = timer.ToString("F0");
            timer -= Time.deltaTime;
            yield return null;
        }
    }

}
