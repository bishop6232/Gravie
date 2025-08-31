using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour, IShopCustomer
{
    UI_Shop shopUI;
    AudioManager audioManager;
    public CollectableManager collectableManager; 
    Vector2 moveInput;
    Rigidbody2D rb;
    public GameManagerScript gameManager;

    public int coinsCollected { get; private set; }
    public int diamondsCollected { get; private set; }

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
        shopUI = FindAnyObjectByType<UI_Shop>();
        gameManager = FindAnyObjectByType<GameManagerScript>();
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
    public void ActivateSpeedBoost(float duration, float speedMultiplier)
    {
        StartCoroutine(SpeedBoostCoroutine(duration, speedMultiplier));
    }
    private IEnumerator SpeedBoostCoroutine(float duration, float speedMultiplier)
    {
        float originalWalkSpeed = walkspeed;
        float originalRunSpeed = runspeed;

        walkspeed += speedMultiplier;
        runspeed += speedMultiplier;

        yield return new WaitForSeconds(duration);

        walkspeed = originalWalkSpeed;
        runspeed = originalRunSpeed;
    }

    public void ActivateMagnet(float duration)
    {
        StartCoroutine(MagnetCoroutine(duration));
    }
    private IEnumerator MagnetCoroutine(float duration)
    {
        collectableManager.ActivateMagnet();
        yield return new WaitForSeconds(duration);
        collectableManager.DeactivateMagnet();
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
            audioManager.PlaySound(audioManager.coinSFX);
            Destroy(other.gameObject);
            collectableManager.coinsCollected++;

            Debug.Log("Coin picked up!");
        }
        if (other.CompareTag("diamond"))
        {
            audioManager.PlaySound(audioManager.coinSFX);
            Destroy(other.gameObject);
            collectableManager.diamondsCollected++;

            Debug.Log("Diamond picked up!");
        }
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Level Complete!");
            gameManager.GameWon();
        }
    }

    public void BoughtItem(Item.ItemType itemType)
    {
        Debug.Log("Player bought item: " + itemType);
        switch (itemType)
        {
            case Item.ItemType.fullHealthPotion:
                // Heal to full health
                GetComponent<PlayerHealth>().Heal(100); // assuming 100 is full health
                break;
            case Item.ItemType.halfHealthPotion:
                // Heal to half health
                GetComponent<PlayerHealth>().Heal(50); // assuming 50 is half health
                break;
            case Item.ItemType.quarterHealthPotion:
                // Heal to quarter health
                GetComponent<PlayerHealth>().Heal(25); // assuming 25 is quarter health
                break;
            case Item.ItemType.magnet:
            // Activate magnet effect
            ActivateMagnet(60f);
                if (shopUI != null)
                {
                    shopUI.Hide();
                }   
                break;
            case Item.ItemType.speedRun:
                // Activate speed boost
                ActivateSpeedBoost(60f, 9f);
                 if(shopUI != null) {
                    shopUI.Hide();
                 }
                break;
                
        }
    }

    public bool TrySpendCoins(int spendCoinsCollected)
    {
        if (collectableManager.coinsCollected >= spendCoinsCollected)
        {
            collectableManager.coinsCollected -= spendCoinsCollected;
            return true;
        }

        Debug.Log("Not enough coins");
        return false;
    }

    public bool TrySpendSpecial(int spendSpecialCollected)
    {
        if (collectableManager.diamondsCollected >= spendSpecialCollected)
        {
            collectableManager.diamondsCollected -= spendSpecialCollected;
            return true;
        }
        Debug.Log("Not enough diamonds");
        return false;
      

    }

}

