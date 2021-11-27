using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Buffer : MonoBehaviour
{
    public List<ComboInput> queue = new List<ComboInput>();

    public ComboList comboList;

    public float clearCooldown;

    public Coroutine clearDelay;

    public PlayerHitsManager phm;

    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(controller.inputProfile.heavyAttack))
        {
            ComboOverloadCheck();
            if (clearDelay != null) StopCoroutine(clearDelay);
            queue.Add(new ComboInput(controller.inputProfile.heavyAttack, TypeOfInput.Movement));
            clearDelay = StartCoroutine(ClearDelay());
            Debug.Log(AdvancedFindCombo()?.comboName);
        }
        if (Input.GetKeyDown(controller.inputProfile.mediumAttack))
        {
            ComboOverloadCheck();
            if(clearDelay != null) StopCoroutine(clearDelay);
            queue.Add(new ComboInput(controller.inputProfile.mediumAttack, TypeOfInput.Hit, 0.05f));
            clearDelay = StartCoroutine(ClearDelay());
            Debug.Log(AdvancedFindCombo()?.comboName);
        }
    }

    public IEnumerator ClearDelay()
    {

        float time = 0f;

        while(time < clearCooldown)
        {
            time += Time.deltaTime;
            yield return null;

        }

        queue.Clear();        
    }

    public void ComboOverloadCheck()
    {
        if (queue.Count > 4) queue.RemoveAt(0);
    }

    public Combo FindCombo(int length)
    {
        foreach(Combo c in comboList.combos)
        {
            if (c.CompileToString().Length > 0 && CompileToString(length).Length > 0)
                if (c.CompileToString() == CompileToString(length))
                {
                    queue.Clear();
                    return c;
                }
        }

        return null;
    }

    public void DisableLastHitsMembers()
    {
        foreach (ComboInput ci in queue)
        {
            if (ci != queue.Last()) phm.GetHitMemberFromType(ci.member).Disable();
        }
    }

    public string CompileToString(int comboLength)
    {

        if (queue.Count < comboLength) return "";

        string result = "";
        int startIndex = queue.Count - comboLength;
      

        for (int i = startIndex; i < queue.Count; i++)
        {
            result += queue[i].key.ToString();
        }

        return result;
    }

    public Combo AdvancedFindCombo()
    {
        DisableLastHitsMembers();

        Combo c3 = FindCombo(3);
        Combo c4 = FindCombo(4);
        Combo c5 = FindCombo(5);

        if (c3 != null) return c3;
        else if (c4 != null) return c4;
        else if (c5 != null) return c5;
        else return null;
    }
}
