using UnityEngine;

// This script checks if the player is touching the ground or walls
public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D CastFilter;
    public float groundDistance = 0.3f;
    public float wallDistance = 0.2f;

    CapsuleCollider2D touchingCollider;
    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];



    [SerializeField]
    private bool isGrounded;
    public bool isGravityInverted = false;


    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
        private set
        {
            isGrounded = value;
            animator.SetBool("isGrounded", isGrounded);
        }
    }
    private bool isOnWall;

    public bool IsOnWall
    {
        get
        {
            return isOnWall;
        }
        private set
        {
            isOnWall = value;
            animator.SetBool("isOnWall", isOnWall);
        }
    }


    // prevent hugging walls by checking the direction based on the player's facing direction
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;



    private void Awake()
    {
        touchingCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        Vector2 castDirection = isGravityInverted ? Vector2.up : Vector2.down;

        IsGrounded = touchingCollider.Cast(castDirection, CastFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCollider.Cast(wallCheckDirection, CastFilter, wallHits, wallDistance) > 0;
    }
}