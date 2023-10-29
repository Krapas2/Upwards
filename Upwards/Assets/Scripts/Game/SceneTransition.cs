using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public string sceneName;

    private PlayerInventory playerInventory;

    void Start(){
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    void OnTriggerEnter2D(Collider2D col){
        foreach(PlayerInventory.Item item in playerInventory.items){
            PlayerPrefs.SetInt (item.name, item.amount);
        }

        PlayerPrefs.SetString ("lastScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(sceneName);
    }
}
