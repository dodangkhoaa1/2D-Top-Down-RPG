using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string characterName;
    public Sprite sprite;
    public GameObject Prefab;
    public string Description;
    public int price;

    [SerializeField]
    private bool isUnlocked;

    public void Unlock()
    {
        isUnlocked = true;
    }

    public bool IsUnlocked() => isUnlocked;
}
