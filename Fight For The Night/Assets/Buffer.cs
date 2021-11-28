using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class Buffer : MonoBehaviour
{
    public List<ComboInput> queue = new List<ComboInput>();

    public ComboInput lowInput;
    public ComboInput mediumInput;
    public ComboInput heavyInput;

    public ComboList comboList;

    public float clearCooldown;

    public Coroutine clearDelay;

    public PlayerHitsManager phm;

    [ReadOnly]
    public ComboInput lastInput;

    [ReadOnly]
    public ComboInput currentInput;

    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (queue.Count > 0) lastInput = queue.Last();
        else lastInput = null;
    }

    public void AddBuffer(ComboInput comboInput, float animationTime)
    {
        ComboOverloadCheck();

        //if (comboInput.animationName == "Uppercut") Debug.Break();

        if (clearDelay != null) StopCoroutine(clearDelay);

        ComboInput ci = new ComboInput(comboInput);
        ci.animationTime = animationTime;
        queue.Add(ci);

        if(ci.animationName == "")
            clearDelay = StartCoroutine(ClearDelay(ci));

        //Debug.Log(AdvancedFindCombo()?.comboName);
        EnableLastBufferInputHit();
    }

    float fullBufferTime = 0f;
    public IEnumerator ClearDelay(ComboInput ci)
    {
        float time = 0f;

        fullBufferTime += ci.CastTime();

        while (time < fullBufferTime)
        {
            time += Time.deltaTime;
            yield return null;

        }

        queue.Clear();
        fullBufferTime = 0f;
    }

    public bool castLock = false;

    public IEnumerator CastCoolDown(float cd)
    {
        castLock = true;
        yield return new WaitForSeconds(cd);
        castLock = false;
    }

    public void EnableLastBufferInputHit()
    {
        if(queue.Count > 0 && !castLock)
        {
            phm.GetHitMemberFromType(currentInput.member).Enable();
            StartCoroutine(CastCoolDown(currentInput.animationTime));
        }
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
                    //queue.Clear();
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

    public Combo AdvancedFindCombo(ComboInput previewInput)
    {
        queue.Add(previewInput);

        Combo c2 = FindCombo(2);
        Combo c3 = FindCombo(3);
        Combo c4 = FindCombo(4);
        Combo c5 = FindCombo(5);

        queue.Remove(previewInput);
        
        if (c5 != null)
        {
            DisableLastHitsMembers();
            return c5;
        }
        else if (c4 != null)
        {
            DisableLastHitsMembers();
            return c4;
        }
        else if (c3 != null)
        {
            DisableLastHitsMembers();
            return c3;
        }
        else if (c2 != null)
        {
            DisableLastHitsMembers();
            return c2;
        }


        return null;
    }

}
