using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    [Header("Selection UI")]
    [SerializeField] private SpriteRenderer characterSpriteRenderer; // preview
    [SerializeField] private List<Sprite> characters = new();        // preview sprites only

    private int selectedIndex = 0;
    private const string PrefKey = "SelectedCharacterIndex";

    private void Start()
    {
        selectedIndex = Mathf.Clamp(PlayerPrefs.GetInt(PrefKey, 0), 0, Mathf.Max(0, characters.Count - 1));
        UpdatePreview();
    }

    public void NextOption()
    {
        if (characters.Count == 0) return;
        selectedIndex = (selectedIndex + 1) % characters.Count;
        UpdatePreview();
    }

    public void PreviousOption()
    {
        if (characters.Count == 0) return;
        selectedIndex = (selectedIndex - 1 + characters.Count) % characters.Count;
        UpdatePreview();
    }

    private void UpdatePreview()
    {
        if (characterSpriteRenderer && characters.Count > 0)
            characterSpriteRenderer.sprite = characters[selectedIndex];
    }

    public void PlayGame()
    {
        Debug.Log("[CharacterManager] Saving index = " + selectedIndex);
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectedIndex);
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlayScene");
    }
    public void BackButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
