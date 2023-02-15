using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    [HideInInspector]
    public TowerVertical[] doors;

    [HideInInspector]
    public bool playerIsInDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerIsInDoor = false;
        List<TowerVertical> tempDoors = new List<TowerVertical>();
        foreach(GameObject door in GameObject.FindGameObjectsWithTag("Door")){
            if(door.GetComponent<Structure>().built){
                TowerVertical thisDoor = door.GetComponent<TowerVertical>();
                tempDoors.Add(thisDoor);
                playerIsInDoor |= thisDoor.playerIsInside;
            }
        }
        doors = tempDoors.ToArray();
    }
}
