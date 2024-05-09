using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDatabase;

    public Text nameText;
    public Text descriptionText;
    public SpriteRenderer artWorkSprite;
    public GameObject artWorkPrefab;

    private int characterOption = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("characterOption"))
        {
            characterOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(characterOption);
    }

    public void NextOption()
    {
        characterOption++;

        if (characterOption >= characterDatabase.characterCount)
        {
            characterOption = 0;
        }

        UpdateCharacter(characterOption);
        Save();
    }
    public void BackOption()
    {
        characterOption--;

        if (characterOption < 0)
        {
            characterOption = characterDatabase.characterCount - 1;
        }

        UpdateCharacter(characterOption);
        Save();
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDatabase.GetCharacter(selectedOption);
        artWorkSprite.sprite = character.sprite;
        artWorkPrefab = character.Prefab;
        nameText.text = character.characterName;
        descriptionText.text = character.Description;
    }

    private void Load()
    {
        characterOption = PlayerPrefs.GetInt("characterOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("characterOption", characterOption);
    }

}
