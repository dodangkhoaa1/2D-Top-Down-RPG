using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Map
{
    public string mapName;
    public Sprite mapSprite;
    public string mapDescription;

    public int price;


    public bool isUnlocked;

    public void Unlock()
    {
        isUnlocked = true;
    }
}
