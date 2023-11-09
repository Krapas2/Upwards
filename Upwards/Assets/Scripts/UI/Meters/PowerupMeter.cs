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

    private bool fading;
    private bool faded;

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
            ShowChildren();
            StopCoroutine(FadeChildren());
        }
        else if (!(fading || faded))
        {
            StartCoroutine(FadeChildren());
        }

        Debug.Log(faded);
        Debug.Log(fading);
        /*
                bool childrenNotFaded = true;
                foreach (Image child in children)
                {
                    childrenNotFaded &= child.color.a != 0;
                }

                if (!Show() && !fading && childrenNotFaded)
                {
                    fading = true;
                    Invoke("FadeChildren", showTime);
                }*/
    }

    public abstract float FillAmount();
    public abstract bool Show();
    /*
        void FadeChildren()
        {
            foreach (Image child in children)
            {
                StartCoroutine(FadeMeter(child));
            }
        }*/

    void ShowChildren()
    {
        ChangeChildrenAlpha(1f);
        faded = false;
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
        fading = true;
        yield return new WaitForSeconds(showTime);

        Color c;
        for (float alpha = 1f; alpha >= 0; alpha -= fadeTime * Time.deltaTime)
        {
            ChangeChildrenAlpha(alpha);

            yield return null;
        }
        foreach (Image child in children)
        {
            c = child.color;
            c.a = 0;
            child.color = c;
        }
        fading = false;
        faded = true;
    }
}
