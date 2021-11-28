using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public bool isPlayingFinalCombo;

    private bool _isPlayingHitAnimationsWithRootMotion;
    public bool isPlayingHitAnimationsWithRootMotion { set { _isPlayingHitAnimationsWithRootMotion = value; } }

    // Start is called before the first frame update
    void Start()
    {
        GetClipsLength();
        _isPlayingHitAnimationsWithRootMotion = false;
    }

    private void Update()
    {
        bar.UpdateBar(_hp);

        #region AnimationHits

        if (!isPlayingFinalCombo)
        {
            if (Input.GetKeyDown(inputProfile.heavyAttack))
            {
                Combo validCombo = inputBuffer.AdvancedFindCombo(inputBuffer.heavyInput);

                if (validCombo != null)
                {
                    if (validCombo.isFinal)
                    {
                        inputBuffer.AddBuffer(validCombo.inputs.Last(), GetClipLengthFromName(validCombo.inputs.Last().animationName));
                        isPlayingFinalCombo = true;
                    }
                    else
                        inputBuffer.AddBuffer(inputBuffer.heavyInput, heavyDuration);
                }
                else
                {
                    if (!_isPlayingHitAnimationsWithRootMotion)
                    {
                        inputBuffer.AddBuffer(inputBuffer.heavyInput, heavyDuration);
                    }
                }
            }

            if (Input.GetKeyDown(inputProfile.mediumAttack))
            {
                Combo validCombo = inputBuffer.AdvancedFindCombo(inputBuffer.mediumInput);

                if (validCombo != null)
                {
                    if (validCombo.isFinal)
                    {
                        inputBuffer.AddBuffer(validCombo.inputs.Last(), GetClipLengthFromName(validCombo.inputs.Last().animationName));
                        isPlayingFinalCombo = true;
                    }
                    else
                        inputBuffer.AddBuffer(inputBuffer.mediumInput, mediumDuration);
                }
                else
                {
                    if (!_isPlayingHitAnimationsWithRootMotion)
                    {
                        inputBuffer.AddBuffer(inputBuffer.mediumInput, mediumDuration);
                    }
                }
            }

            if (Input.GetKeyDown(inputProfile.lightAttack))
            {
                Combo validCombo = inputBuffer.AdvancedFindCombo(inputBuffer.lowInput);

                if (validCombo != null)
                {
                    if (validCombo.isFinal)
                    {
                        inputBuffer.AddBuffer(validCombo.inputs.Last(), GetClipLengthFromName(validCombo.inputs.Last().animationName));
                        isPlayingFinalCombo = true;
                    }
                    else
                        inputBuffer.AddBuffer(inputBuffer.lowInput, lightDuration);
                }
                else
                {
                    if (!_isPlayingHitAnimationsWithRootMotion)
                    {
                        inputBuffer.AddBuffer(inputBuffer.lowInput, lightDuration);
                    }
                }
            }

        }

        if(!_isPlayingHitAnimationsWithRootMotion)
        {
            ReadBuffer();
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

    public void ReadBuffer()
    {
        for (int i = 0; i < inputBuffer.queue.Count; i++)
        {
            ComboInput ci = inputBuffer.queue[i];
            
            if (!ci.hasBeenPlayed)
            {
                if (ci.animationName != "")
                {
                    animator.SetTrigger(ci.animationName);
                    inputBuffer.queue.Clear();
                }
                else animator.SetTrigger(SimpleHitAnimationFromEnum(ci.hitType));

                ci.hasBeenPlayed = true;
            }
        }
    }

    public void TakeDamages(int value)
    {
        _hp -= value;
    }

    float lightDuration, mediumDuration, heavyDuration;
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
                case "dfg":
                    heavyDuration = clip.length / animator.GetFloat("HeavySpeed");
                    break;
                case "sjfgdj":
                    heavyDuration = clip.length / animator.GetFloat("HeavySpeed");
                    break;
                case "sdfghsdfh":
                    heavyDuration = clip.length / animator.GetFloat("HeavySpeed");
                    break;
                case "fgdjhfgd,":
                    heavyDuration = clip.length / animator.GetFloat("HeavySpeed");
                    break;
                case "hsdfghsdghj":
                    heavyDuration = clip.length / animator.GetFloat("HeavySpeed");
                    break;                    
            }
        }
    }

    public float GetClipLengthFromName(string name)
    {
        switch(name)
        {
            case "":
                return lightDuration;
        }

        return default;
    }

    public string SimpleHitAnimationFromEnum(HitType hit)
    {
        switch (hit)
        {
            case HitType.Light:
                return "LightHit";
            case HitType.Medium:
                return "NormalHit";
            case HitType.Heavy:
                return "HeavyHit";
        }

        return "";
    }
}
