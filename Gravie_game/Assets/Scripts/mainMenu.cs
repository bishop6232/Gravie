using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject SettingsUI;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    
    }
    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quitGame()
    {
        Application.Quit();
        System.Diagnostics.Process.GetCurrentProcess().Kill();
        Debug.Log("QUIT");
    }
    public void Options()
    {
        if (SettingsUI != null)
        {
            SettingsUI.SetActive(true);
        }
    }
    public void CloseOptions()
    {
        if (SettingsUI != null)
        {
            SettingsUI.SetActive(false);
        }
    }
 
}
