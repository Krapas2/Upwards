using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Image loadingBar;
    void Start()
    {
        StartCoroutine(LoadAsynchronously(PlayerPrefs.GetString("nextScene")));
    }

    IEnumerator LoadAsynchronously(string sceneName){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone){
            loadingBar.fillAmount = operation.progress;
            yield return null;
        }
        PlayerPrefs.SetString("nextScene", "Cunk");
        yield return null;
    }
}
