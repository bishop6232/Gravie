using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    int score = 0;
    int highScore = 0;

    void Start()
    {
        scoreText.text = "Current Score:"+ " " + score.ToString();
        highScoreText.text = "Highest Score: " + highScore.ToString();
        
    }
}
