using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int id;
    public float speed;

    public Transform self;
    public Rigidbody selfRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        selfRigidbody.velocity = self.forward * x * speed * Time.deltaTime;
    }
}
