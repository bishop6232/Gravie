using UnityEngine;
public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] private int damageAmount = 5;

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
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
