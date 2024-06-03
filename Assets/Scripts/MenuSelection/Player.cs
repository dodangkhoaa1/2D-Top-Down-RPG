using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    private void Start()
    {
        int characterOption = Prefs.characterSelectedOption;
        Instantiate(prefabs[characterOption], transform.position, Quaternion.identity);
        Destroy(gameObject);
        Debug.Log(DatabaseKey.UnlockCharacter);
    }
}
