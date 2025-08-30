using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text winOrLoseText;
    public GameObject gameOverUI;

    public GameObject ScoreBoardUI;

    public GameObject pauseMenuUI;

    public GameObject selectedPlayer;

    public GameObject CurrentPlayer;

    private Sprite playerSprite;

    AudioManager audioManager;

    void Start()
    {
        if (selectedPlayer != null)
        {
            playerSprite = selectedPlayer.GetComponent<SpriteRenderer>().sprite;

            CurrentPlayer.GetComponent<SpriteRenderer>().sprite = playerSprite;
        }
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void EndGame(bool won)
    {
        if (winOrLoseText != null)
        {
            winOrLoseText.text = won ? "Victory!" : "GAME OVER!";
        }


        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            InputSystem.DisableDevice(Keyboard.current);
            InputSystem.DisableDevice(Gamepad.current);
        }
        else InputSystem.EnableDevice(Keyboard.current);
            InputSystem.EnableDevice(Gamepad.current);
            
        if (audioManager != null)
        {
            audioManager.StopSound();
            audioManager.PauseBackgroundMusic();
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
        audioManager.StopBackgroundMusic();
       
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        InputSystem.EnableDevice(Keyboard.current);
        InputSystem.EnableDevice(Gamepad.current);
        audioManager.ResumeBackgroundMusic();
        Time.timeScale = 1f; // Ensure the game is not paused
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
        Debug.Log("Game is quitting");

    }

    public void LoadMainMenu()
    {
        audioManager.StopBackgroundMusic();
        // Load the main menu scene
        SceneManager.LoadScene("Main Menu");
        InputSystem.EnableDevice(Keyboard.current);
        InputSystem.EnableDevice(Gamepad.current);
        Debug.Log("Loading Main Menu");
        audioManager.ResumeBackgroundMusic();

        Time.timeScale = 1f; // Ensure the game is not paused


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
    public void LoadPauseMenu()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            InputSystem.DisableDevice(Keyboard.current);
            InputSystem.DisableDevice(Gamepad.current);
            if (audioManager != null)
            {
                audioManager.PauseBackgroundMusic();
            }
        }
    }
    public void ResumeGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f; // Resume the game
            InputSystem.EnableDevice(Keyboard.current);
            InputSystem.EnableDevice(Gamepad.current);
            if (audioManager != null)
            {
                audioManager.ResumeBackgroundMusic();
            }
        }
    }
    
}
