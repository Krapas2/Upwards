using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSwap : MonoBehaviour
{
    private AudioManager _audManager;

    private void Start()
    {
        Invoke("Buffer", 0.2f);
    }

    public void Buffer()
    {
        _audManager = FindObjectOfType<AudioManager>();

        if (PlayerPrefs.GetString("lastScene") != null)
        {
            if (_audManager)
            {
                _audManager.Stop(PlayerPrefs.GetString("lastScene"));
                _audManager.Play(SceneManager.GetActiveScene().name);
            }
        }
    }
}