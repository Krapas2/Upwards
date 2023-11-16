using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public string itemRequired;
    public Sprite fixedSprite;
   // public Sprite brokenSprite;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(PlayerPrefs.GetInt(itemRequired + "PowerupEnabled") == 0){
            StartCoroutine(FixerCoroutine());
        }else{
            spriteRenderer.sprite = fixedSprite;
            Destroy(this);
        }
    }

    IEnumerator FixerCoroutine()
    {
        yield return WaitForFixed();

        spriteRenderer.sprite = fixedSprite;

        Destroy(this);
    }

    IEnumerator WaitForFixed()
    {
        while (PlayerPrefs.GetInt(itemRequired + "PowerupEnabled") == 0)
        {
            yield return null;
        }
        yield return null;
    }
}
