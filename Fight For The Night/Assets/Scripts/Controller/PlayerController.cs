using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float backwardSpeed;
    [SerializeField]
    private int _hp = 200;
    public int hp { get { return _hp; } }

    [Header("References")]
    public Transform self;
    public Rigidbody selfRigidbody;
    public Transform rig;
    public Animator animator;
    public InputProfile inputProfile;

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
        #region AnimationHits
        if (Input.GetKey(inputProfile.heavyAttack) && !_isPlayingHitAnimations)
        {
            animator.SetTrigger("Hit");
            rig.localPosition = Vector3.zero;
            animator.applyRootMotion = false;
            _isPlayingHitAnimations = true;
        }
        #endregion

        // Get Movement
        float x = Input.GetAxis(inputProfile.horizontalAxis);

        if ((x < 0.1 && x > -0.1) && !_isPlayingHitAnimations)
            animator.applyRootMotion = true;
        else
        {
            animator.applyRootMotion = false;
            //rig.localPosition = Vector3.zero;
        }

        #region AnimationMovements
        if (x < 0.0f)
        {
            // Apply movement
            selfRigidbody.velocity = Vector3.right * x * backwardSpeed * Time.deltaTime;
        }
        else if (x >= 0.0f)
        {
            // Apply movement
            selfRigidbody.velocity = Vector3.right * x * speed * Time.deltaTime;
        }

        // Update Anim
        animator.SetFloat("Speed", selfRigidbody.velocity.x, 0.1f, Time.deltaTime);
        #endregion
    }
}
