using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameManagerScript : MonoBehaviour
{
    public InputActionAsset inputActions; 
    [SerializeField] private TMP_Text winOrLoseText;
    public GameObject gameOverUI;

    public GameObject ScoreBoardUI;
    public void EndGame(bool won)
    {
        if (winOrLoseText != null)
        {
            winOrLoseText.text = won ? "You Win!" : "You Lose!";
        }


        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            InputSystem.DisableDevice(Keyboard.current);
            InputSystem.DisableDevice(Gamepad.current);  
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

    public void LoadScoreboard()
    {
        if (ScoreBoardUI != null)
        {
            ScoreBoardUI.SetActive(true);

            if (gameOverUI != null)
            {
                gameOverUI.SetActive(false);
            }
        }
    }
    public void BackToGameOver()
    {
        if (ScoreBoardUI != null)
        {
            ScoreBoardUI.SetActive(false);

            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }
        }
    }
    
}
