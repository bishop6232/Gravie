using UnityEngine;
using TMPro;

public class CollectableManager : MonoBehaviour
{
    [Header("Counts + UI")]
    public int coinsCollected;
    public TMP_Text coinText;
    public TMP_Text totalCoinText;
    public int diamondsCollected;
    public TMP_Text diamondText;

    [Header("Magnet Settings")]
    [Tooltip("Auto-found in Awake via tag 'Player'.")]
    public Transform player;

    [Tooltip("Layer(s) of collectables. Create a 'Collectable' layer and assign coins/diamonds to it.")]
    public LayerMask collectableLayers;          // MUST be set to only your collectables

    public float magnetRadius = 15f;
    public float pullAcceleration = 30f;         // for Dynamic bodies
    public float maxPullSpeed = 12f;             // cap for Dynamic bodies & direct/kinematic moves

    [Tooltip("Shows current state; set via ActivateMagnet/DeactivateMagnet.")]
    public bool magnetActive = false;

    [Header("Filtering (by Tag)")]
    public string coinTag = "coin";
    public string diamondTag = "diamond";

    // Reused buffer to avoid GC allocations each frame
    private readonly Collider2D[] _overlaps = new Collider2D[64];

    private void Awake()
    {
        // Auto-find the player by tag if not assigned
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                player = p.transform;
            }
            else Debug.LogWarning("CollectableManager: No GameObject with tag 'Player' found. Magnet will be inactive.");
        }

        // If no layers set, try common names; otherwise warn
        if (collectableLayers == 0)
        {
            int guess = LayerMask.GetMask("Collectable", "Collectables", "Coins", "Diamonds");
            if (guess != 0) collectableLayers = guess;
            else Debug.LogWarning("CollectableManager: 'collectableLayers' is 0. Set it to ONLY your collectable layers.");
        }
    }

    private void Start()
    {
        coinsCollected = 0;
        diamondsCollected = 0;

        if (coinText != null)
        {
            coinText.text = coinsCollected.ToString();
        }
        if (totalCoinText != null)
        {
            totalCoinText.text = "Total Coins: " + coinsCollected;
        }
        if (diamondText != null)
        {
            diamondText.text = diamondsCollected.ToString();
        }
    }

    private void Update()
    {
        if (coinText != null)
        {
            coinText.text = coinsCollected.ToString();
        }
        if (diamondText != null)
        {
            diamondText.text = diamondsCollected.ToString();
        }
        if (totalCoinText != null)
        {
            totalCoinText.text = "Total Coins: " + coinsCollected;
        }

        if (magnetActive && player != null && collectableLayers != 0)
        {
            AttractNearbyCollectables();
        }
    }

    public void ActivateMagnet()
    {
        magnetActive = true;
        Debug.Log("Magnet activated.");
    }

    public void DeactivateMagnet()
    {
        magnetActive = false;
        Debug.Log("Magnet deactivated.");
    }

    private void AttractNearbyCollectables()
    {
        // Build contact filter (include triggers)
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(collectableLayers);
        filter.useLayerMask = true;
        filter.useTriggers = true;

        // Fill buffer, get count
        int count = Physics2D.OverlapCircle(player.position, magnetRadius, filter, _overlaps);

        for (int i = 0; i < count; i++)
        {
            Collider2D col = _overlaps[i];
            if (col == null)
            {
                continue;
            }

            // Ignore the player and their children
            if (col.transform == player || col.transform.IsChildOf(player))
            {
                continue;
            }

            // Only coins or diamonds
            if (!col.CompareTag(coinTag) && !col.CompareTag(diamondTag))
            {
                continue;
            }

            Transform t = col.transform;
            Vector2 toPlayer = (Vector2)player.position - (Vector2)t.position;
            if (toPlayer.sqrMagnitude < 1e-6f)
            {
                continue;
            }

            Vector2 dir = toPlayer.normalized;

            Rigidbody2D rb = col.attachedRigidbody;
            if (rb == null)
            {
                // No rigidbody → move transform
                t.position = Vector2.MoveTowards(t.position, player.position, maxPullSpeed * Time.deltaTime);
            }
            else
            {
                switch (rb.bodyType)
                {
                    case RigidbodyType2D.Dynamic:
                    {
                        // Accelerate towards player, clamp speed
                        Vector2 newVel = rb.linearVelocity + dir * (pullAcceleration * Time.deltaTime);
                        if (newVel.magnitude > maxPullSpeed)
                            newVel = newVel.normalized * maxPullSpeed;
                        rb.linearVelocity = newVel;
                        break;
                    }
                    case RigidbodyType2D.Kinematic:
                    {
                        // Kinematic bodies ignore forces/velocity; move position directly
                        Vector2 next = Vector2.MoveTowards(rb.position, (Vector2)player.position, maxPullSpeed * Time.deltaTime);
                        rb.MovePosition(next);
                        break;
                    }
                    case RigidbodyType2D.Static:
                    default:
                    {
                        // Static bodies can't move via physics—fallback to transform
                        t.position = Vector2.MoveTowards(t.position, player.position, maxPullSpeed * Time.deltaTime);
                        break;
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (player == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(player.position, magnetRadius);
    }
}
