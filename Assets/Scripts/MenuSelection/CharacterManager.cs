using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private CharacterDatabase characterDatabase;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    //[SerializeField] private SpriteRenderer artWorkSprite;
    [SerializeField] private Image artWorkSprite;
    [SerializeField] private Button buyCharacterButton;

    private int characterOption;
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
        Debug.Log(characterOption);
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
        Debug.Log(characterOption);
        UpdateCharacter(characterOption);
        UpdateUI(); // Cập nhật giao diện người dùng khi chọn nhân vật mới
    }

    public void UnlockCharacter()
    {
            if (economyManager != null)
            {
                Character character = characterDatabase.GetCharacter(characterOption);
                if (!character.IsUnlocked() && Prefs.IsEnoughCoins(character.price))
                {
                    economyManager.DeductGold(character.price);
                    character.Unlock(); // Mở khóa nhân vật
                    characterDatabase.UpdateCharacterUnlockedStatus(characterOption); // Cập nhật trạng thái mở khóa trong CharacterDatabase
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

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDatabase.GetCharacter(selectedOption);
        artWorkSprite.sprite = character.sprite;
        nameText.text = character.characterName;
        descriptionText.text = character.Description;
        SaveCharacterOption();
    }

    private void LoadCharacterOption()
    {
        characterOption = Prefs.characterSelectedOption;
    }

    private void SaveCharacterOption()
    {
        Prefs.characterSelectedOption = characterOption;
    }
    private void UpdateUI()
    {
        Character character = characterDatabase.GetCharacter(characterOption);

        if (character != null)
        {
            if (character.IsUnlocked())
            {
                buyCharacterButton.gameObject.SetActive(false);
            }
            else
            {
                buyCharacterButton.GetComponentInChildren<TextMeshProUGUI>().text = "-$" + character.price;
                if (economyManager != null && Prefs.IsEnoughCoins(character.price))
                {
                    buyCharacterButton.interactable = true;
                }
                else
                {
                    buyCharacterButton.interactable = false;
                }
                buyCharacterButton.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Character is null. Check if characterDatabase.GetCharacter(characterOption) returns a valid character.");
        }
    }
}
