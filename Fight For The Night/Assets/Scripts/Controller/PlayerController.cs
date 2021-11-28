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
        _isPlayingHitAnimationsWithRootMotion = false;
        indexBuffer = 0;
    }

    private void Update()
    {
        bar.UpdateBar(_hp);

        #region AnimationHits
        if (Input.GetKeyDown(inputProfile.heavyAttack))
        {
            inputBuffer.AddBuffer(inputBuffer.heavyInput);
        }

        if (Input.GetKeyDown(inputProfile.mediumAttack))
        {
            inputBuffer.AddBuffer(inputBuffer.mediumInput);
        }

        if (Input.GetKeyDown(inputProfile.lightAttack))
        {
            inputBuffer.AddBuffer(inputBuffer.lowInput);
        }
        #endregion

        if (inputBuffer.queue.Count < indexBuffer)
        {
            indexBuffer = 0;
        }

        if (!_isPlayingHitAnimationsWithRootMotion && inputBuffer.queue.Count != 0 && indexBuffer < inputBuffer.queue.Count)
        {
            KeyCode key = inputBuffer.queue[indexBuffer].key;
            switch (key)
            {
                case KeyCode k when k == inputProfile.lightAttack:
                    animator.SetTrigger("LightHit");
                    break;
                case KeyCode k when k == inputProfile.mediumAttack:
                    animator.SetTrigger("NormalHit");
                    break;
                case KeyCode k when k == inputProfile.heavyAttack:
                    animator.SetTrigger("HeavyHit");
                    break;
                default:
                    break;
            }

            animator.applyRootMotion = true;
            _isPlayingHitAnimationsWithRootMotion = true;

            ++indexBuffer;
        }
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
}
