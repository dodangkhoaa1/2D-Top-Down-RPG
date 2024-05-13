using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public MapDatabase mapDatabase;
     public CharacterDatabase characterDatabase;
    public CharacterManager characterManager;
    public Text nameScene;
    public Text descriptionText;
    public SpriteRenderer mapSprite;
    public Button buyMapBtn;

    private EconomyManager economyManager;
    public int mapOption = 0;
    void Start()

    {
        economyManager = EconomyManager.Instance;
        LoadMapOption();
        UpdateCharacter(mapOption);
        UpdateUIMap(); // Cập nhật giao diện người dùng khi bắt đầu
    }

    public void NextOption()
    {
        mapOption++;

        if (mapOption >= mapDatabase.mapCount)
        {
            mapOption = 0;
        }

        UpdateCharacter(mapOption);
        UpdateUIMap();
    }

    public void BackOption()
    {
        mapOption--;

        if (mapOption < 0)
        {
            mapOption = mapDatabase.mapCount - 1;
        }

        UpdateCharacter(mapOption);
        UpdateUIMap();
    }

    private void UpdateCharacter(int selectedOption)
    {
        Map map = mapDatabase.GetMap(selectedOption);
        mapSprite.sprite = map.mapSprite;
        nameScene.text = map.mapName;
        descriptionText.text = map.mapDescription;
    }

    private void LoadMapOption()
    {
        mapOption = PlayerPrefs.GetInt("mapOption", 0);
    }

    private void SaveMapOption()
    {
        PlayerPrefs.SetInt("mapOption", mapOption);
        PlayerPrefs.Save();
    }
    public void UnlockMap()
    {
        if (economyManager != null)
        {
            if (economyManager != null)
            {
                Map map = mapDatabase.GetMap(mapOption);
                if (!map.isUnlocked && economyManager.currentGold >= map.price)
                {
                    economyManager.DeductGold(map.price);
                    map.Unlock(); // Mở khóa nhân vật
                    SaveMapOption();
                    mapDatabase.UpdateMapUnlockedStatus(mapOption, true); // Cập nhật trạng thái mở khóa trong CharacterDatabase
                    UpdateUIMap(); // Cập nhật giao diện người dùng khi mở khóa nhân vật
                }
                else
                {
                    Debug.Log("Cannot buy map: not enough gold or map already unlocked.");
                }
            }
            else
            {
                Debug.LogError("EconomyManager is not assigned!");
            }
        }
    }
    private void UpdateUIMap()
    {
        Map map = mapDatabase.GetMap(mapOption);

        if (map != null)
        {
            if (map.isUnlocked)
            {
                buyMapBtn.gameObject.SetActive(false);

            }
            else
            {
                buyMapBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Buy-" + map.price;
                if (economyManager != null && economyManager.currentGold >= map.price)
                {
                    buyMapBtn.interactable = true;
                }
                else
                {
                    buyMapBtn.interactable = false;
                }
                buyMapBtn.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Map is null.");
        }
    }
    public void ChangeScene()
    {
        Character character = characterDatabase.GetCharacter(characterManager.characterOption);
        if (character != null && character.isUnlocked)
        {
            if (!string.IsNullOrEmpty(nameScene.text))
            {
                SceneManager.LoadScene(nameScene.text);
            }
            else
            {
                Debug.Log("Cannot change scene: Scene name is not provided.");
            }
        }
        else
        {
            Debug.Log("Cannot change scene: Map is not unlocked.");
        }
    }
}
