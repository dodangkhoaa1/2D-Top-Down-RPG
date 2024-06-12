using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private float waitToLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemies = GameObject.Find("Enemies");
        Debug.Log(enemies.transform.childCount);
        if (enemies.transform.childCount == 0)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                SceneManagement.Instance.SetTransitionName(sceneTransitionName);
                UIFade.Instance.FadeToBlack();
                StartCoroutine(LoadSceneRoutine());
            }
        }
        
    }

    private IEnumerator LoadSceneRoutine()
    {
        while (waitToLoadTime >= 0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }
        if (sceneToLoad.Equals(SceneNameConsts.SELECTION_MENU))
        {
            List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag(TagConsts.UIPLAYING_TAG).ToList();
            gameObjects.Add(GameObject.FindWithTag("Player"));
            foreach (var item in gameObjects)
            {
                Destroy(item.gameObject);
            }
        }
        SceneManager.LoadScene(sceneToLoad);

    }
}
