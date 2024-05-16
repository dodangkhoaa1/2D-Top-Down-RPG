using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    private void Start()
    {
        int characterOption = PlayerPrefs.GetInt(DatabaseKey.CharacterSelectedOptionKey);
        Instantiate(prefabs[characterOption]);
        Destroy(gameObject);
        Debug.Log(DatabaseKey.UnlockCharacter);
    }
}
