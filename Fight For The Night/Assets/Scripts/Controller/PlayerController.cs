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
    private bool _isCurrentAnimIsFinished;
    public bool isCurrentAnimIsFinished { set { _isCurrentAnimIsFinished = value; } }
    private bool _isMakingForwardStep;
    public bool isMakingForwardStep { set { _isMakingForwardStep = value; } }
    private bool _isStepDone;
    public bool isStepDone { set { _isStepDone = value; } }

    // Start is called before the first frame update
    void Start()
    {
        canPressInput = true;
        _isPlayingHitAnimationsWithRootMotion = false;
        _isCurrentAnimIsFinished = false;
        _isMakingForwardStep = false;
        _isStepDone = false;
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
        if (!_isStepDone && !_isMakingForwardStep && moveX > 0.0f)
        {
            selfRigidbody.velocity = Vector3.right * 1.0f * speed * Time.deltaTime;
            _isMakingForwardStep = true;
            animator.SetFloat("Speed", selfRigidbody.velocity.x);
        }
        if (!_isMakingForwardStep)
        {
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
                _isStepDone = false;
                selfRigidbody.velocity = Vector3.zero;
                if (lastMoveX == moveX)
                {
                    animator.SetFloat("Speed", moveX, 0.02f, Time.deltaTime);
                }
            }
        }

        _lastMoveX = moveX;
        #endregion

        /*if (_isCurrentAnimIsFinished)
        {
            _isCurrentAnimIsFinished = false;
            rig.localPosition = Vector3.zero;
        }*/
    }

    public void TakeDamages(int value)
    {
        _hp -= value;
    }
}
