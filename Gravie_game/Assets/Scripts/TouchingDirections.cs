using UnityEngine;

// This component detects whether the player character is grounded by casting a downward check from its CapsuleCollider2D
public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D CastFilter;
    public float groundDistance = 0.3f; // Distance to check for ground
    CapsuleCollider2D touchingCollider;
    Animator animator;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    [SerializeField]
    private bool isGrounded;

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
        set
        {
            isGrounded = value;
            animator.SetBool("isGrounded", isGrounded);
        }
    }
    private void Awake()
    {
        touchingCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        IsGrounded = touchingCollider.Cast(Vector2.down, CastFilter, groundHits, groundDistance) > 0;
    }
}
