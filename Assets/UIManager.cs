using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI countEnemiesText;
    GameObject enemies;
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        enemies = GameObject.Find("Enemies");
        int count = enemies.transform.childCount;
        if(count == 0 )
        {
            countEnemiesText.text = $"The door was opened!";
        }else
        {
            countEnemiesText.text = $"{count} enemies are alive";
        }
    }
}
