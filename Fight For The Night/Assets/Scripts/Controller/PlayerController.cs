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
    public float cooldownBetweenTwoInputs;

    [Header("References")]
    public Transform self;
    public Rigidbody selfRigidbody;
    public Transform rig;
    public Animator animator;
    public InputProfile inputProfile;

    private float _lastMoveX;
    public float lastMoveX { get { return _lastMoveX; } }

    public HP_Bar bar;

    private bool canPressInput;
    private bool _isPlayingHitAnimationsWithRootMotion;
    public bool isPlayingHitAnimationsWithRootMotion { set { _isPlayingHitAnimationsWithRootMotion = value; } }

    // Start is called before the first frame update
    void Start()
    {
        canPressInput = true;
        _isPlayingHitAnimationsWithRootMotion = false;
    }

    IEnumerator CooldownBetweenTwoInputs()
    {
        canPressInput = false;
        yield return new WaitForSeconds(cooldownBetweenTwoInputs);
        canPressInput = true;
    }

    private void Update()
    {
        bar.UpdateBar(_hp);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region AnimationHits
        if (Input.GetKey(inputProfile.heavyAttack) && canPressInput)
        {
            animator.SetTrigger("HeavyHit");
            //rig.localPosition = Vector3.zero;
            animator.applyRootMotion = true;
            _isPlayingHitAnimationsWithRootMotion = true;
            StartCoroutine(CooldownBetweenTwoInputs());
        }

        if (Input.GetKey(inputProfile.mediumAttack) && canPressInput)
        {
            animator.SetTrigger("NormalHit");
            //rig.localPosition = Vector3.zero;
            animator.applyRootMotion = true;
            _isPlayingHitAnimationsWithRootMotion = true;
            StartCoroutine(CooldownBetweenTwoInputs());
        }

        if (Input.GetKey(inputProfile.lightAttack) && canPressInput)
        {
            animator.SetTrigger("LightHit");
            //rig.localPosition = Vector3.zero;
            animator.applyRootMotion = true;
            _isPlayingHitAnimationsWithRootMotion = true;
            StartCoroutine(CooldownBetweenTwoInputs());
        }
        #endregion

        // Get Movement
        float moveX = Input.GetAxis(inputProfile.horizontalAxis);

        if ((moveX < 0.1 && moveX > -0.1) || _isPlayingHitAnimationsWithRootMotion)
            animator.applyRootMotion = true;
        else
        {
            animator.applyRootMotion = false;
        }

        #region AnimationMovements
        if (moveX != 0.0f)
        {
            if (moveX < 0.0f)
            {
                // Apply movement
                selfRigidbody.velocity = Vector3.right * moveX * backwardSpeed * Time.deltaTime;
            }
            else if (moveX > 0.0f)
            {
                // Apply movement
                selfRigidbody.velocity = Vector3.right * moveX * speed * Time.deltaTime;
            }

            // Update Anim
            animator.SetFloat("Speed", selfRigidbody.velocity.x, 0.02f, Time.deltaTime);
        }
        else
        {
            selfRigidbody.velocity = Vector3.zero;
            if (lastMoveX == moveX)
            {
                animator.SetFloat("Speed", moveX, 0.02f, Time.deltaTime);
            }
        }

        _lastMoveX = moveX;
        #endregion
    }

    public void TakeDamages(int value)
    {
        _hp -= value;
    }
}
