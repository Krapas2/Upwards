using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [System.Serializable]
    public struct Shot
    {
        public Sprite shotImage;
        public float time;
    }
    public Shot[] shots;

    public float fadeTime;
    public float waitTime;

    public string nextScene;

    private int currentShotIndex = 0;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();

        Color c = image.color;
        c.a = 0;
        image.color = c;

        StartCoroutine(PlayShot(0));
    }

    IEnumerator PlayShot(int shotIndex)
    {
        image.sprite = shots[shotIndex].shotImage;
        yield return new WaitForSeconds(waitTime);
        yield return FadeShot(true);
        yield return new WaitForSeconds(shots[shotIndex].time);
        yield return FadeShot(false);

        currentShotIndex++;
        if (currentShotIndex < shots.Length)
        {
            PlayNextShot();
        }
        else
        {
            yield return new WaitForSeconds(waitTime);
            PlayerPrefs.SetString("lastScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(nextScene);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(nextScene);
    }

    void PlayNextShot()
    {
        StartCoroutine(PlayShot(currentShotIndex));
    }


    IEnumerator FadeShot(bool fadeIn)
    {
        Color c = image.color;
        for (float timer = 0f; timer < fadeTime; timer += Time.deltaTime)
        {
            float progress = timer / fadeTime;
            c.a = fadeIn ? progress : 1 - progress;
            image.color = c;
            yield return null;
        }
    }
}
