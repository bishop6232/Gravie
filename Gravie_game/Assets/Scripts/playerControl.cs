using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]

public class playerControl : MonoBehaviour
{
    Vector2 moveInput;

    Rigidbody2D rb;
    Animator animator;


    private bool isMoving = false;

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

    private bool isRunning = false;

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
    public float walkspeed = 4f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }



    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * walkspeed, rb.linearVelocity.y);
    }
    // function to handle player movement input
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        FacingDirection(moveInput);
    }
    
    // function to change the player's facing direction based on input
    private void FacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    

    // function to handle player running input
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
}
