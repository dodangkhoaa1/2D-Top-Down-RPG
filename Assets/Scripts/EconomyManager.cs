using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    [SerializeField] private TMP_Text goldText;
    public int currentGold = 0;

    private const string PLAYER_GOLD_KEY = "PlayerGold"; // Key để lưu số lượng coin

    private void Start()
    {
        LoadPlayerGold(); // Nạp số lượng coin từ PlayerPrefs khi game bắt đầu
        if (goldText != null)
        {
            UpdateGoldText();
        }
        else
        {
            Debug.LogWarning("Gold Amount Text reference is not set!");
        }
    }

    public int GetCurrentGold()
    {
        return currentGold;
    }

    public void UpdateCurrentGold(int amount = 1)
    {
        currentGold += amount;
        SavePlayerGold(); // Lưu số lượng coin sau mỗi lần thay đổi
        UpdateGoldText();  
    }
    /// <summary>
    /// Cập nhật tiền hiển thị trên UI
    /// </summary>
    private void UpdateGoldText()
    {
        if (goldText != null)
        {
            goldText.text = currentGold.ToString("D3");
        }
    }
    /// <summary>
    /// Lưu tiền vào biến toàn cục
    /// </summary>
    private void SavePlayerGold()
    {
        PlayerPrefs.SetInt(PLAYER_GOLD_KEY, currentGold);
        PlayerPrefs.Save(); // Lưu dữ liệu vào PlayerPrefs
    }

    /// <summary>
    /// Load tiền từ biến toàn cục 
    /// </summary>
    public void LoadPlayerGold()
    {
        if (PlayerPrefs.HasKey(PLAYER_GOLD_KEY))
        {
            currentGold = PlayerPrefs.GetInt(PLAYER_GOLD_KEY);
        }
    }
    /// <summary>
    /// Sử dụng tiền 
    /// </summary>
    /// <param name="amount"></param>
    public void DeductGold(int amount)
    {
        currentGold -= amount;
        UpdateGoldText();
        SavePlayerGold();
    }
}
