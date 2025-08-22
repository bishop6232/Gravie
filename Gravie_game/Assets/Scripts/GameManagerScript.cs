using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text winOrLoseText;
    public GameObject gameOverUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

  public void EndGame(bool won)
    {
        if (winOrLoseText != null)
        {
            winOrLoseText.text = won ? "You Win!" : "You Lose!"; 
        }


        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
            
    }
    public void GameOver()
    {
        EndGame(false);
    }
    public void GameWon()
    {
        EndGame(true);
    }

    public void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
        Debug.Log("Game is quitting");
    }

    public void LoadMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("Main Menu");
    }
    
}
