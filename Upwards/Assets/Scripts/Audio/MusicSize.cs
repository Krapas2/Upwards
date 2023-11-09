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
        _audManager.Play("CaveNoises");
    }

    void Update()
    {
        float volume, volume2;
        volume = Map(player.position.y, maxHeight, minHeight, 0f, 0.5f);
        volume2 = Map(player.position.y, maxHeight, minHeight, 1f, 0f);

        volume = Mathf.Clamp01(volume);

        if (_audManager)
        { 
            _audManager.SetVolume("Wilds", volume);
            _audManager.SetVolume("CaveNoises", volume2);
        }


    }

    public float Map(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax)
    {
        float OldRange = OldMax - OldMin;
        float NewRange = NewMax - NewMin;
        float NewValue = ((OldValue - OldMin) * NewRange / OldRange) + NewMin;

        return NewValue;
    }
}
