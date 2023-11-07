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
    public LayerMask ground;

    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        for(int i = 0; i < playerInventory.items.Length; i++){
            playerInventory.items[i].amount = PlayerPrefs.GetInt (playerInventory.items[i].name);
        }
        SetPlayerOnSpawn();
    }

    public void SetPlayerOnSpawn(){
        foreach(SpawnPoint spawnPoint in spawnPoints){
            if(PlayerPrefs.GetString ("lastScene") == spawnPoint.SceneName){
                Vector3 pos = (Vector3)Physics2D.Raycast(spawnPoint.point.position, Vector2.down, Mathf.Infinity, ground).point;
                PlayerMovement player = FindObjectOfType<PlayerMovement>();
                Vector3 playerDisplacement = player.groundCheck.position - player.transform.position;
                player.transform.position = pos - playerDisplacement;
            }
        }
    }
}
