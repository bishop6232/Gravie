using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    AudioManager audioManager;
    public CollectableManager collectableManager;
    Vector2 moveInput;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;

    Animator animator;
    public float walkspeed = 3f;
    public float runspeed = 7f;
    public float jumpImpulse = 10f;

    private bool isMoving = false;
    private bool isRunning = false;

    private bool isGravityInverted = false;


    public float CurrentMoveSpeed
    {
        get
        {
            if (isMoving && !touchingDirections.IsOnWall)
            {
                if (isRunning)
                {
                    return runspeed;
                }
                else
                {
                    return walkspeed;
                }

            }
            else
            {
                return 0;
            }
        }
    }
    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
        private set
        {
            isMoving = value;
            animator.SetBool("isMoving", isMoving);

        }
    }

    public bool IsRunning
    {
        get
        {
            return isRunning;
        }
        private set
        {
            isRunning = value;
            animator.SetBool("isRunning", isRunning);
        }
    }
    public bool isFacingRight = true; // player faces right by default

    public bool IsFacingRight
    {
        get
        {
            return isFacingRight;
        }
        private set
        {
            if (isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            isFacingRight = value;
        }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }

    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;

        }
    }

    // TODO: check if player is still alive before jump
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger("jump");

            float direction = isGravityInverted ? -1f : 1f; // negative when inverted
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse * direction);
        }
    }


    public void OnFlipGravity(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            StartCoroutine(JumpThenFlip());
        }
    }

    private IEnumerator JumpThenFlip()
    {
        // jump direction depends on gravity
        float jumpDirection = isGravityInverted ? -1f : 1f;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse * jumpDirection);

        yield return new WaitForSeconds(0.15f);

        FlipGravity();
    }
    private void FlipGravity()
    {
        // Invert gravity and update the player's scale

        isGravityInverted = !isGravityInverted;
        rb.gravityScale *= -1;

        Vector2 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;


        touchingDirections.isGravityInverted = isGravityInverted;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("coin"))
        {
            audioManager.PlayCoinSound(audioManager.coinSFX);
            Destroy(other.gameObject);
            collectableManager.coinsCollected++;

            Debug.Log("Coin picked up!");
        }
    }
}
