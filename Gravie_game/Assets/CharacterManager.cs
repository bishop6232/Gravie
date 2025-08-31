using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


public class CharacterManager : MonoBehaviour
{
    public SpriteRenderer characterSpriteRenderer;
    public List<Sprite> characters = new List<Sprite>(); // List of character sprites
    private int selectedPlayer = 0;
    public GameObject playerCharacter;

    public void NextOption()
    {
        selectedPlayer = selectedPlayer + 1;
        if (selectedPlayer == characters.Count)
        {
            selectedPlayer = 0;
        }

        characterSpriteRenderer.sprite = characters[selectedPlayer];
    }

    public void BackOption()
    {
        selectedPlayer = selectedPlayer - 1;
        if (selectedPlayer < 0)
        {
            selectedPlayer = characters.Count - 1;
        }

        characterSpriteRenderer.sprite = characters[selectedPlayer];
    }

    public void PlayGame()
    {
        PrefabUtility.SaveAsPrefabAsset(playerCharacter, "Assets/selectedPlayer.prefab");
        SceneManager.LoadScene("GamePlayScene");
    }

}