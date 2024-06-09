using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private readonly string SelectionSceneName = "SelectionMenu";
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    public void Resume()
    {
        SceneManager.LoadScene(SelectionSceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
