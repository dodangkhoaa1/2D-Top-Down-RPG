using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private readonly string SelectionSceneName = "SelectionMenu";
    private readonly string CutSceneName = "CutScene";
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
    public void Storyline()
    {
        SceneManager.LoadScene(CutSceneName);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
