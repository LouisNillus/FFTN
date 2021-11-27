using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int id;
    public float speed;

    public Transform self;
    public Rigidbody selfRigidbody;
    public Animator animator;

    private Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        originalRotation = self.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        if (x > -0.1 && x < 0.1)
            animator.applyRootMotion = true;
        else
            animator.applyRootMotion = false;
        selfRigidbody.velocity = self.forward * x * speed * Time.deltaTime;
        animator.SetFloat("Speed", x * speed * Time.deltaTime);
        self.rotation = originalRotation;
    }
}
