using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public MapDatabase mapDatabase;

    public Text nameScene;
    public Text descriptionText;
    public SpriteRenderer mapSprite;

    private int mapOption = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("mapOption"))
        {
            mapOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(mapOption);
    }

    public void NextOption()
    {
        mapOption++;

        if (mapOption >= mapDatabase.mapCount)
        {
            mapOption = 0;
        }

        UpdateCharacter(mapOption);
        Save();
    }
    public void BackOption()
    {
        mapOption--;

        if (mapOption < 0)
        {
            mapOption = mapDatabase.mapCount - 1;
        }

        UpdateCharacter(mapOption);
        Save();
    }

    private void UpdateCharacter(int selectedOption)
    {
        Map map = mapDatabase.GetMap(selectedOption);
        mapSprite.sprite = map.mapSprite;
        nameScene.text = map.mapName;
        descriptionText.text = map.mapDescription;
    }

    private void Load()
    {
        mapOption = PlayerPrefs.GetInt("mapOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("mapOption", mapOption);
    }


    public void ChangeScene()
    {
        SceneManager.LoadScene(nameScene.text);
    }
}
