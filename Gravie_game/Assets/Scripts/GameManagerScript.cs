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


    [Header("Player Setup")]
    [SerializeField] private Animator playerAnimator;                         // assign the Player’s Animator
    [SerializeField] private List<RuntimeAnimatorController> characterControllers; // [0]=Player1 controller, [1]=Player2 AOC, etc.

    private const string PrefKey = "SelectedCharacterIndex";
    private AudioManager audioManager;

    private void Awake()
    {
        InputSystem.EnableDevice(Keyboard.current);
        InputSystem.EnableDevice(Gamepad.current);
        audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();

        // Apply chosen controller BEFORE animations tick
        if (playerAnimator != null && characterControllers != null && characterControllers.Count > 0)
        {
            int idx = Mathf.Clamp(PlayerPrefs.GetInt(PrefKey, 0), 0, characterControllers.Count - 1);
            playerAnimator.runtimeAnimatorController = characterControllers[idx];
        }
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
        else
            {
            InputSystem.EnableDevice(Keyboard.current);
            InputSystem.EnableDevice(Gamepad.current);
        }

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
        InputSystem.EnableDevice(Keyboard.current);
        InputSystem.EnableDevice(Gamepad.current);
        // Load the main menu scene
        SceneManager.LoadScene("Main Menu");
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
