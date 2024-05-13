using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    private const string SELECTION_MAP_OPTION = "mapOption";
    [SerializeField] private MapDatabase mapDatabase;
    [SerializeField] private CharacterDatabase characterDatabase;
    [SerializeField] private CharacterManager characterManager;
    [SerializeField] private Text nameScene;
    [SerializeField] private Text descriptionText;
    [SerializeField] private SpriteRenderer mapSprite;
    [SerializeField] private Button buyMapBtn;

    private EconomyManager economyManager;
    private int mapOption;
    void Start()

    {
        EconomyManager.Instance.LoadPlayerGold(); //Tải tiền từ database 
        economyManager = EconomyManager.Instance;
        LoadMapOption();
        UpdateMap(mapOption);
        UpdateUIMap(); // Cập nhật giao diện người dùng khi bắt đầu
    }

    public void NextOption()
    {
        mapOption++;

        if (mapOption >= mapDatabase.mapCount)
        {
            mapOption = 0;
        }

        UpdateMap(mapOption);
        UpdateUIMap();
    }

    public void BackOption()
    {
        mapOption--;

        if (mapOption < 0)
        {
            mapOption = mapDatabase.mapCount - 1;
        }

        UpdateMap(mapOption);
        UpdateUIMap();
    }

    private void UpdateMap(int selectedOption)
    {
        Map map = mapDatabase.GetMap(selectedOption);
        mapSprite.sprite = map.mapSprite;
        nameScene.text = map.mapName;
        descriptionText.text = map.mapDescription;
        SaveMapOption();
    }
    /// <summary>
    /// get previous map option, if not return 0 
    /// </summary>
    private void LoadMapOption()
    {
        mapOption = PlayerPrefs.GetInt(SELECTION_MAP_OPTION, 0);
    }

    /// <summary>
    /// Lưu option map giữa các scene 
    /// </summary>
    private void SaveMapOption()
    {
        PlayerPrefs.SetInt(SELECTION_MAP_OPTION, mapOption);
        PlayerPrefs.Save();
    }
    public void UnlockMap()
    {
        if (economyManager != null)
        {
            Map map = mapDatabase.GetMap(mapOption);
            if (!map.isUnlocked)
            {
                if (economyManager.currentGold >= map.price)
                {
                    economyManager.DeductGold(map.price);
                    map.Unlock(); // Mở khóa nhân vật
                    mapDatabase.UpdateMapUnlockedStatus(mapOption, true); // Cập nhật trạng thái mở khóa trong CharacterDatabase
                    UpdateUIMap(); // Cập nhật giao diện người dùng khi mở khóa nhân vật }
                }
                else
                {
                    Debug.Log("Cannot buy map: not enough gold.");
                }
            }
            else
            {
                Debug.Log("Cannot buy map: map already unlocked.");
            }
        }
        else
        {
            Debug.LogError("EconomyManager is not assigned!");
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
        Character character = characterDatabase.GetCharacter(PlayerPrefs.GetInt("characterOption"));
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
