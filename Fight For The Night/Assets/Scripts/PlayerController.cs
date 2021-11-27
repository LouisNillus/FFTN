using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public int id;
    public float speed;
    public float backwardSpeed;
    [SerializeField]
    private int _hp = 200;
    public int hp { get { return _hp; } }

    [Header("References")]
    public Transform self;
    public Rigidbody selfRigidbody;
    public Animator animator;

    //private 

    private bool _isPlayingHitAnimations;
    public bool isPlayingHitAnimations { set { _isPlayingHitAnimations = value; } }

    // Start is called before the first frame update
    void Start()
    {
        _isPlayingHitAnimations = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get Movement
        float x = Input.GetAxis("Horizontal");

        #region AnimationMovements
        // Set root motion if the player is not moving
        if ((x < 0.1 && x > -0.1) && !_isPlayingHitAnimations)
            animator.applyRootMotion = true;
        else
            animator.applyRootMotion = false;

        if (x < -0.1f)
        {
            // Apply movement
            selfRigidbody.velocity = Vector3.right * x * backwardSpeed * Time.deltaTime;
            // Update Anim
            animator.SetFloat("Speed", x * backwardSpeed * Time.deltaTime);
        }
        else if (x > 0.1f)
        {
            // Apply movement
            selfRigidbody.velocity = Vector3.right * x * speed * Time.deltaTime;
            // Update Anim
            animator.SetFloat("Speed", x * speed * Time.deltaTime);
        }
        else
        {
            // Set speed to 0
            selfRigidbody.velocity = Vector3.zero;
            animator.SetFloat("Speed", 0.0f);
        }
        #endregion

        #region AnimationHits
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Hit");
            animator.applyRootMotion = false;
            _isPlayingHitAnimations = true;
        }
        #endregion
    }
}
