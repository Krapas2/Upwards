using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerupManager : MonoBehaviour
{
    [System.Serializable]
    public struct Powerup
    {
        public MonoBehaviour powerup;
        public string ItemRequiredName;
        public int ItemRequiredAmount;
    }

    public Powerup[] powerups;

    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }

    

    public int PowerupIndexFromItemName(string checkName)
    {
        for (int i = 0; i < powerups.Length; i++)
        {
            if (checkName == powerups[i].ItemRequiredName)
            {
                return i;
            }
        }
        return -1;
    }

    public bool HasAllPowerups(){
        bool output = true;
        foreach(Powerup powerup in powerups){
            output &= powerup.powerup.enabled;
        }
        return output;
    }
}
