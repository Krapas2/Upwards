using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public string sceneName;
    private AudioManager _audManager;
    private PlayerInventory playerInventory;

    void Start(){
        playerInventory = FindObjectOfType<PlayerInventory>();
        _audManager = FindObjectOfType<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D col){
        foreach(PlayerInventory.Item item in playerInventory.items){
            PlayerPrefs.SetInt (item.name, item.amount);
        }

        _audManager.Stop(SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString ("lastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString ("nextScene", sceneName);
        SceneManager.LoadScene("LoadingScene");
    }
}
