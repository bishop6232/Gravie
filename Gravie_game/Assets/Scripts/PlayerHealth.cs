using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth;

   
    // Invoked with (currentHealth, maxHealth)
    public UnityEvent<int, int> OnHealthChanged = new UnityEvent<int, int>();

    // Read-only properties (for safe access from other scripts)
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    private Animator animator;
    private PlayerController playerController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();

        currentHealth = maxHealth;
        animator.SetBool("isAlive", true);

        // Fire once so UI can initialize if it’s already listening
        OnHealthChanged.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0)
        {
           return; 
        } 

        currentHealth = Mathf.Max(0, currentHealth - amount);
        OnHealthChanged.Invoke(currentHealth, maxHealth);

        if (currentHealth == 0)
        {
            Die();
        }
            
    }
    /*
    public void Heal(int amount)
    {
        if (currentHealth <= 0)
        {
            return; 
        } 

        int newHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (newHealth != currentHealth)
        {
            currentHealth = newHealth;
            OnHealthChanged.Invoke(currentHealth, maxHealth);
        }
    } */

    private void Die()
    {
        animator.SetBool("isAlive", false);
        if (playerController != null)
        {
            playerController.enabled = false; // Disable player controls
        } 
        
    }
}
