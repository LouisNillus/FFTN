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

        if (Input.GetKeyDown(controller.inputProfile.heavyAttack))
        {
            ComboOverloadCheck();
            if (clearDelay != null) StopCoroutine(clearDelay);


            queue.Add(new ComboInput(heavyInput));
            clearDelay = StartCoroutine(ClearDelay());
            Debug.Log(AdvancedFindCombo()?.comboName);
            EnableLastBufferInputHit();
        }
        if (Input.GetKeyDown(controller.inputProfile.mediumAttack))
        {
            ComboOverloadCheck();
            if(clearDelay != null) StopCoroutine(clearDelay);
            queue.Add(new ComboInput(mediumInput));
            clearDelay = StartCoroutine(ClearDelay());
            Debug.Log(AdvancedFindCombo()?.comboName);
            EnableLastBufferInputHit();
        }
        if (Input.GetKeyDown(controller.inputProfile.lightAttack))
        {
            ComboOverloadCheck();
            if (clearDelay != null) StopCoroutine(clearDelay);
            queue.Add(new ComboInput(lowInput));
            clearDelay = StartCoroutine(ClearDelay());
            Debug.Log(AdvancedFindCombo()?.comboName);
            EnableLastBufferInputHit();
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
            phm.GetHitMemberFromType(queue.Last().member).Enable();
            StartCoroutine(CastCoolDown(queue.Last().castTime));
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

        Combo c3 = FindCombo(3);
        Combo c4 = FindCombo(4);
        Combo c5 = FindCombo(5);

        if (c3 != null)
        {
            DisableLastHitsMembers();
            return c3;
        }
        else if (c4 != null)
        {
            DisableLastHitsMembers();
            return c4;
        }
        else if (c5 != null)
        {
            DisableLastHitsMembers();
            return c5;
        }
        else return null;
    }
}
