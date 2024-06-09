using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private MapDatabase mapDatabase;
    [SerializeField] private CharacterDatabase characterDatabase;
    [SerializeField] private CharacterManager characterManager;
    [SerializeField] private TextMeshProUGUI nameScene;
    [SerializeField] private TextMeshProUGUI descriptionText;
    //[SerializeField] private SpriteRenderer mapSprite;
    [SerializeField] private Image mapSprite;
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
        mapOption = Prefs.mapSeletedOption;
    }

    /// <summary>
    /// Lưu option map giữa các scene 
    /// </summary>
    private void SaveMapOption()
    {
        Prefs.mapSeletedOption = mapOption;
    }
    public void UnlockMap()
    {
        if (economyManager != null)
        {
            Map map = mapDatabase.GetMap(mapOption);
            if (!map.IsUnlocked())
            {
                if (Prefs.IsEnoughCoins(map.price) )
                {
                    economyManager.DeductGold(map.price);
                    map.Unlock(); // Mở khóa nhân vật

                    mapDatabase.UpdateMapUnlockedStatus(mapOption); // Cập nhật trạng thái mở khóa trong CharacterDatabase
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
            if (map.IsUnlocked())
            {
                buyMapBtn.gameObject.SetActive(false);
            }
            else
            {
                buyMapBtn.GetComponentInChildren<TextMeshProUGUI>().text = "-$" + map.price;
                if (economyManager != null && Prefs.IsEnoughCoins(map.price))
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

        Character character = characterDatabase.GetCharacter(Prefs.characterSelectedOption);
        Map map = mapDatabase.GetMap(Prefs.mapSeletedOption);
        if (map.IsUnlocked())
        {
            if (character.IsUnlocked())
            {
                SceneManager.LoadScene(nameScene.text);
            }
            else
            {
                Debug.Log("Character is locked. Please unlock character to continue!");
            }
        }
        else
        {
            Debug.Log("Map is locked. Please unlock map to continue!");
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
