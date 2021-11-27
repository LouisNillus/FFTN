using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffer : MonoBehaviour
{
    public List<ComboInput> queue = new List<ComboInput>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) queue.Add(new ComboInput(KeyCode.D, TypeOfInput.Movement));
        if (Input.GetKeyDown(KeyCode.G)) queue.Add(new ComboInput(KeyCode.G, TypeOfInput.Hit, 0.05f));
    }
}
