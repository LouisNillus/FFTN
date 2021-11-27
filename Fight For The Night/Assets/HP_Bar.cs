using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Bar : MonoBehaviour
{
    public Image armorBar;
    public Image lifeBar;
    public Image borderBar;
    public bool fixedBorder;

    public int maxHP;
    public int hpTest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBar(hpTest);
    }

    public void UpdateBar(int hp)
    {
        if(hp >= (maxHP/2f))
        {
            armorBar.fillAmount = (float)((hp - (maxHP/2f)) / (maxHP / 2f));
            lifeBar.fillAmount = 1f;

            if(!fixedBorder)
            borderBar.fillAmount = 1f;
        }

        if (hp <= (maxHP/2f))
        {
            armorBar.fillAmount = 0f;
            lifeBar.fillAmount = (float)(hp / (maxHP / 2f));

            if(!fixedBorder)
            borderBar.fillAmount = (float)(hp / (maxHP / 2f));          
        }
        
    }
}
