using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSize : MonoBehaviour
{
    public Transform player;
    public float minHeight;
    public float maxHeight;
    private AudioManager _audManager;
    public enum Scene {Wilds, Tower}
    public Scene scene;
    void Start()
    {
        _audManager = FindObjectOfType<AudioManager>();
            if (scene == Scene.Wilds)
                _audManager.Play("CaveNoises");
            
            if (scene == Scene.Tower)
                _audManager.Play("Golden Cloud");
            
    }

    void Update()
    {
        float volume, volume2;
        if (_audManager)
        { 
            if (scene == Scene.Wilds){     
                volume = Map(player.position.y, maxHeight, minHeight, 0f, 0.5f);
                volume2 = Map(player.position.y, maxHeight, minHeight, 1f, 0f);
                volume = Mathf.Clamp01(volume);

                _audManager.SetVolume("Wilds", volume);
                _audManager.SetVolume("CaveNoises", volume2);
            }

            if (scene == Scene.Tower){
                volume = Map(player.position.y, maxHeight, minHeight, 0f, 0.5f);
                volume2 = Map(player.position.y, maxHeight, minHeight, 0.2f, 0f);
                volume = Mathf.Clamp01(volume);

                _audManager.SetVolume("Tower", volume);
                _audManager.SetVolume("Golden Cloud", volume2);
            }
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
