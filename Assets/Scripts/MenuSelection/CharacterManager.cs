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
    public Button buycharButton;

    public int characterOption = 0;
    private EconomyManager economyManager; // Tham chiếu đến EconomyManager
    void Start()
    {
        economyManager = EconomyManager.Instance;
        LoadCharacterOption();
        UpdateCharacter(characterOption);
        UpdateUI(); // Cập nhật giao diện người dùng khi bắt đầu
    }

    public void NextOption()
    {
        characterOption++;
        if (characterOption >= characterDatabase.characterCount)
        {
            characterOption = 0;
        }
        UpdateCharacter(characterOption);
        UpdateUI(); // Cập nhật giao diện người dùng khi chọn nhân vật mới
    }

    public void BackOption()
    {
        characterOption--;
        if (characterOption < 0)
        {
            characterOption = characterDatabase.characterCount - 1;
        }
        UpdateCharacter(characterOption);
        UpdateUI(); // Cập nhật giao diện người dùng khi chọn nhân vật mới
    }

    public void UnlockCharacter()
    {
        if (economyManager != null)
        {
            if (economyManager != null)
            {
                Character character = characterDatabase.GetCharacter(characterOption);
                if (!character.isUnlocked && economyManager.currentGold >= character.price)
                {
                    economyManager.DeductGold(character.price);
                    character.Unlock(); // Mở khóa nhân vật
                    SaveCharacterOption();
                    characterDatabase.UpdateCharacterUnlockedStatus(characterOption, true); // Cập nhật trạng thái mở khóa trong CharacterDatabase
                    UpdateUI(); // Cập nhật giao diện người dùng khi mở khóa nhân vật
                }
                else
                {
                    Debug.Log("Cannot buy character: not enough gold or character already unlocked.");
                }
            }
            else
            {
                Debug.LogError("EconomyManager is not assigned!");
            }
        }
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDatabase.GetCharacter(selectedOption);
        artWorkSprite.sprite = character.sprite;
        artWorkPrefab = character.Prefab;
        nameText.text = character.characterName;
        descriptionText.text = character.Description;
    }

    private void LoadCharacterOption()
    {
        characterOption = PlayerPrefs.GetInt("characterOption", 0);
    }

    private void SaveCharacterOption()
    {
        PlayerPrefs.SetInt("characterOption", characterOption);
        PlayerPrefs.Save();
    }
    private void UpdateUI()
    {
        Character character = characterDatabase.GetCharacter(characterOption);

        if (character != null)
        {
            if (character.isUnlocked)
            {
                buycharButton.gameObject.SetActive(false);

            }
            else
            {
                buycharButton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy-" + character.price;
                if (economyManager != null && economyManager.currentGold >= character.price)
                {
                    buycharButton.interactable = true;
                }
                else
                {
                    buycharButton.interactable = false;
                }
                buycharButton.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Character is null. Check if characterDatabase.GetCharacter(characterOption) returns a valid character.");
        }
    }
}
