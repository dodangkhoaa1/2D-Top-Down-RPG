using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverDialog : Dialog
{
    const string TOWN_TEXT = "CharacterSelectScene";
    public void Replay()
    {
        Show(false);
        PlayerHealth.Instance.StartRespawn();
    }
    public void BackMenu()
    {
        Show(false );
        PlayerHealth.Instance.HideGameUI();
        LoadMenuScene();
    }
    public void ExitGame()
    {
        Show(false );
       Application.Quit();
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(TOWN_TEXT);
    }
}
