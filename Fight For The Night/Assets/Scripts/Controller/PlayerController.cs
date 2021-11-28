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
    public Buffer inputBuffer;

    private float _lastMoveX;
    public float lastMoveX { get { return _lastMoveX; } }

    public HP_Bar bar;

    private int indexBuffer;

    private bool _isPlayingHitAnimationsWithRootMotion;
    public bool isPlayingHitAnimationsWithRootMotion { set { _isPlayingHitAnimationsWithRootMotion = value; } }

    // Start is called before the first frame update
    void Start()
    {
        GetClipsLength();
        _isPlayingHitAnimationsWithRootMotion = false;
        indexBuffer = 0;
    }

    private void Update()
    {
        bar.UpdateBar(_hp);

        #region AnimationHits
        if (Input.GetKeyDown(inputProfile.heavyAttack) && !_isPlayingHitAnimationsWithRootMotion)
        {
            Combo validCombo = inputBuffer.AdvancedFindCombo(inputBuffer.heavyInput);

            if(validCombo == null)
            {
                animator.SetTrigger("HeavyHit");
                IsPlayingAnimation();
                inputBuffer.AddBuffer(inputBuffer.heavyInput, heavyDuration);
            }
            else
            {

            }

        }

        if (Input.GetKeyDown(inputProfile.mediumAttack) && !_isPlayingHitAnimationsWithRootMotion)
        {
            Combo validCombo = inputBuffer.AdvancedFindCombo(inputBuffer.mediumInput);
            if (validCombo == null)
            {
                animator.SetTrigger("NormalHit");
                IsPlayingAnimation();
                inputBuffer.AddBuffer(inputBuffer.mediumInput, mediumDuration);
            }
            else
            {

            }
        }

        if (Input.GetKeyDown(inputProfile.lightAttack) && !_isPlayingHitAnimationsWithRootMotion)
        {
            Combo validCombo = inputBuffer.AdvancedFindCombo(inputBuffer.lowInput);
            if (validCombo == null)
            {
                animator.SetTrigger("LightHit");
                IsPlayingAnimation();
                inputBuffer.AddBuffer(inputBuffer.lowInput, lightDuration);
            }
            else
            {
                
            }
        }
        #endregion

    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

    void IsPlayingAnimation()
    {
        _isPlayingHitAnimationsWithRootMotion = true;
    }

    public float lightDuration, mediumDuration, heavyDuration;
    public void GetClipsLength()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "LightPunch":
                    lightDuration = clip.length / animator.GetFloat("LightSpeed");
                    break;
                case "NormalPunch":
                    mediumDuration = clip.length / animator.GetFloat("MediumSpeed");
                    break;
                case "HighKick":
                    heavyDuration = clip.length / animator.GetFloat("HeavySpeed");
                    break;
            }
        }
    }
}
