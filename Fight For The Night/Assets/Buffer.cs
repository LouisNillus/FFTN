using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Buffer : MonoBehaviour
{
    public List<ComboInput> queue = new List<ComboInput>();

    public ComboList allCombos;

    public float clearCooldown;

    public Coroutine clearDelay;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(allCombos.combos[0].CompileToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ComboOverloadCheck();
            if (clearDelay != null) StopCoroutine(clearDelay);
            queue.Add(new ComboInput(KeyCode.D, TypeOfInput.Movement));
            clearDelay = StartCoroutine(ClearDelay());

        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ComboOverloadCheck();
            if(clearDelay != null) StopCoroutine(clearDelay);
            queue.Add(new ComboInput(KeyCode.G, TypeOfInput.Hit, 0.05f));
            clearDelay = StartCoroutine(ClearDelay());

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
        foreach(Combo c in allCombos.combos)
        {
            if (c.CompileToString().Length > 0 && CompileToString(length).Length > 0)
                if (c.CompileToString() == CompileToString(length))
                {
                    Debug.Log(c.comboName);
                    queue.Clear();
                    return c;
                }
        }

        return null;
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
}
