using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSpawn : MonoBehaviour
{

    [System.Serializable]
    public struct SpawnPoint{
        public string SceneName;
        public Transform point;
    }

    public SpawnPoint[] spawnPoints;

    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        for(int i = 0; i < playerInventory.items.Length; i++){
            playerInventory.items[i].amount = PlayerPrefs.GetInt (playerInventory.items[i].name);
        }
        
        foreach(SpawnPoint spawnPoint in spawnPoints){
            if(PlayerPrefs.GetString ("lastScene") == spawnPoint.SceneName){
                GameObject.FindGameObjectWithTag("Player").transform.position = spawnPoint.point.position;
            }
        }
    }
}
