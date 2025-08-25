using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth;


    // Invoked with (currentHealth, maxHealth)
    public UnityEvent<int, int> OnHealthChanged;
    public GameManagerScript gameManager;
    private bool isDead;
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
    
        currentHealth = Mathf.Max(0, currentHealth - amount);
        OnHealthChanged.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true; // Prevent multiple calls to Die
            Die();
            gameManager.GameOver(); // Call GameOver method from GameManagerScript
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
    }
}

