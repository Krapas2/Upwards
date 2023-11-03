using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSwap : MonoBehaviour
{
    private AudioManager _audManager;

    private void Start()
    {
        _audManager = FindObjectOfType<AudioManager>();

        if (PlayerPrefs.GetString("lastScene") != null)
        {
            if (_audManager)
            {
                _audManager.Play(SceneManager.GetActiveScene().name);
            }
        }
    }

}
