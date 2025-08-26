using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public CollectableManager collectableManager;
    public TMP_Text currentScoreText;
    public TMP_Text highestScoreText;

    int highScore = 0;

    void Start()
    {
        // Load saved high score (default 0 if none exists)
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Update the UI right away
        if (highestScoreText != null)
            highestScoreText.text = "Highest Score: " + highScore;
    }

    public void UpdateScore()
    {
        // Show current score
        if (currentScoreText != null)
            currentScoreText.text = "Current Score: " + collectableManager.coinsCollected;

        // Only update if the player beat the high score
        if (collectableManager.coinsCollected > highScore)
        {
            highScore = collectableManager.coinsCollected;

            // Save new high score permanently
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

            // Update UI
            if (highestScoreText != null)
                highestScoreText.text = "Highest Score: " + highScore;
        }
    }
}
