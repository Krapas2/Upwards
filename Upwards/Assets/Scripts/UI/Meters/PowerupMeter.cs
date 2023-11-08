using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PowerupMeter : MonoBehaviour
{
    public string powerupButton;
    public float showTime;
    public float fadeTime;
    public Image meter;

    private Image[] children;
    public void Setup()
    {
        children = GetComponentsInChildren<Image>();

        Invoke("FadeInvoke", showTime);
    }

    void Update()
    {
        meter.fillAmount = FillAmount();

        if (Show())
        {
            foreach (Image child in children)
            {
                Color c = child.color;
                c.a = 1f;
                child.color = c;
            }
            CancelInvoke();
        }
        if (Hide())
        {
            Invoke("FadeInvoke", showTime);
        }
    }

    public abstract float FillAmount();
    public abstract bool Show();
    public abstract bool Hide();

    void FadeInvoke()
    {
        foreach (Image child in children)
        {
            StartCoroutine(FadeMeter(child));
        }
    }

    IEnumerator FadeMeter(Image image)
    {
        Color c = image.color;
        for (float alpha = 1f; alpha >= 0; alpha -= fadeTime * Time.deltaTime)
        {
            c.a = alpha;
            image.color = c;

            yield return null;
        }
        c.a = 0;
        image.color = c;
    }
}
