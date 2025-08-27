using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public CollectableManager collectableManager;
    public TMP_Text currentScoreText;
    public TMP_Text highestScoreText;

    private int highScore = 0;

    void Start()
    {
        // Load saved high score 
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (highestScoreText != null)
            highestScoreText.text = "Highest Score: " + highScore;
    }

    public void UpdateScore()
    {
        int currentScore = collectableManager.coinsCollected;

        if (currentScoreText != null)
            currentScoreText.text = "Current Score: " + currentScore;

        // to check and update high score if current score exceeds it
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentScore > savedHighScore)
        {
            // to save new high score
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
            highScore = currentScore;
        }
        else
        {
            highScore = savedHighScore;
        }

        if (highestScoreText != null)
        {
            highestScoreText.text = "Highest Score: " + highScore;
        }
    }
}