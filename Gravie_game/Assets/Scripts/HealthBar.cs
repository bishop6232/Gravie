using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text healthBarText;
    [SerializeField] private Slider healthBarSlider;

    private PlayerHealth playerHealth;

    private void Awake()
    {
        // Find the player and cache reference
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("HealthBar: No GameObject with tag 'Player' found in the scene.");
            return;
        }

        playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("HealthBar: PlayerHealth component not found on the Player.");
            return;
        }
    }

    private void OnEnable()
    {
        if (playerHealth != null)
        {
             playerHealth.OnHealthChanged.AddListener(OnPlayerHealthChanged);
        }
           
    }

    private void Start()
    {
        // Initialize UI to the player's current values
        if (playerHealth != null)
        {
             OnPlayerHealthChanged(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        }
           
    }

    private void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged.RemoveListener(OnPlayerHealthChanged);
        }
            
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        // Avoid divide-by-zero; set slider to normalized value
        float healthPercentage = maxHealth > 0 ? (float)newHealth / maxHealth : 0f;

        if (healthBarSlider != null)
        {
            healthBarSlider.value = healthPercentage;
        }

        if (healthBarText != null)
        {
            healthBarText.text = $"HP {newHealth}/{maxHealth}";
        }
            
    }
}
