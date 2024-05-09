using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    private void Start()
    {
        int characterOption = PlayerPrefs.GetInt("characterOption");
        Instantiate(prefabs[characterOption]);
    }
}
