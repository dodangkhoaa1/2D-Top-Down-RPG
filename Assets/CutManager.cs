using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutManager : MonoBehaviour
{
    private bool enablePressEscap = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnableEscapeAfterDelay(53.7f));
        StartCoroutine(LoadSceneAfterDelay("MainMenu", 60f));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && enablePressEscap)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    public IEnumerator LoadSceneAfterDelay(string sceneName, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator EnableEscapeAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        enablePressEscap = true;
    }
}
