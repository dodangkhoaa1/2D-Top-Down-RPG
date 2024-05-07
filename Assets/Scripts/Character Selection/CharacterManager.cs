using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDatabase;

    public Text nameText;
    public SpriteRenderer artWorkSprite;
    public GameObject artWorkPrefab;

    private int selectedOption = 0;
    // Start is called before the first frame update
    void Start()
    {
        UpdateCharacter(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= characterDatabase.characterCount)
        {
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
    }
    public void BackOption()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = characterDatabase.characterCount - 1;
        }

        UpdateCharacter(selectedOption);
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDatabase.GetCharacter(selectedOption);
        artWorkSprite.sprite = character.sprite;
        artWorkPrefab = character.Prefab;
        nameText.text = character.characterName;
    }
}
