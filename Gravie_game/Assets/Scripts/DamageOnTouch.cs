using UnityEngine;
public class DamageOnTouch : MonoBehaviour
{
    Collider2D fireCollider;
    AudioManager audioManager;
    [SerializeField] private int damageAmount = 5;

    private void Awake()
    {
        fireCollider = GetComponent<Collider2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        // this will get the PlayerHealth script.
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            return;
        }
        // Apply damage to the player
        else
            audioManager.PlaySound(audioManager.impactSFX);
        playerHealth.TakeDamage(damageAmount);


        Debug.Log($"Player collided with {gameObject.name} and took {damageAmount} damage.");
        if (playerHealth.CurrentHealth <= 0)
        {
            Debug.Log("Player has died.");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // same as the above but for trigger enabled object
        if (!fireCollider.enabled) { return; }
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                audioManager.PlaySound(audioManager.impactSFX);
                playerHealth.TakeDamage(damageAmount);
            }

        }
    }
   public void EnableDamage(){
        fireCollider.enabled = true;
    }

    public void DisableDamage()
    {
        fireCollider.enabled = false;
    }
}
