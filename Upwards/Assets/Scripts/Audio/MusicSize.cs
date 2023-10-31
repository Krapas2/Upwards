using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSize : MonoBehaviour
{
    public Transform player;

    public float minHeight;
    public float maxHeight;

    private AudioManager _audManager;

    void Start()
    {
        _audManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        float volume;
        volume = Map(player.position.y, maxHeight, minHeight, 0f, 0.5f);

        volume = Mathf.Clamp01(volume);

        _audManager.SetVolume("Wilds", volume);


    }

    public float Map(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax)
    {
        float OldRange = OldMax - OldMin;
        float NewRange = NewMax - NewMin;
        float NewValue = ((OldValue - OldMin) * NewRange / OldRange) + NewMin;

        return NewValue;
    }
}
