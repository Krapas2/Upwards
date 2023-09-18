using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VignetteSize : MonoBehaviour
{
    public Transform player;

    public float minHeight;
    public float maxHeight;
    public float minSize;
    public float maxSize;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        float vignetteSize;
        vignetteSize = Mathf.Clamp(Map(player.position.y, maxHeight, minHeight, minSize, maxSize), minSize, maxSize);
        Debug.Log(vignetteSize);

        image.material.SetFloat("_WindowSize", vignetteSize);
        image.material.SetVector("_PlayerPos", player.position - transform.position);
    }

    public float Map(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax){
        float OldRange = OldMax - OldMin;
        float NewRange = NewMax - NewMin;
        float NewValue = ((OldValue - OldMin) * NewRange / OldRange) + NewMin;
        
        return NewValue;
    }
}
