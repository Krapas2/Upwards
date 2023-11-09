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


        ShowChildren();
        StartCoroutine(FadeChildren());
    }

    void Update()
    {
        meter.fillAmount = FillAmount();

        if (Show())
        {
            StopAllCoroutines();
            ShowChildren();
            StartCoroutine(FadeChildren());
        }
    }

    public abstract float FillAmount();
    public abstract bool Show();

    void ShowChildren()
    {
        ChangeChildrenAlpha(1f);
    }

    void HideChildren()
    {
        ChangeChildrenAlpha(0f);
    }

    void ChangeChildrenAlpha(float alpha)
    {
        foreach (Image child in children)
        {
            Color c = child.color;
            c.a = alpha;
            child.color = c;
        }
    }

    IEnumerator FadeChildren()
    {
        yield return new WaitForSeconds(showTime);

        for (float alpha = 1f; alpha >= 0; alpha -= fadeTime * Time.deltaTime)
        {
            ChangeChildrenAlpha(alpha);

            yield return null;
        }
        HideChildren();
    }
}