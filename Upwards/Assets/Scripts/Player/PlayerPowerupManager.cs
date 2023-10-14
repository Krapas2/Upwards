using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerupManager : MonoBehaviour
{
    public enum PowerupType {Broom, Bomb, Cloud};

    [System.Serializable]
    public struct Powerup
    {
        public PowerupType powerup;
        public string ItemRequiredName;
        public int ItemRequiredAmount;
    }

    public Powerup[] powerups;

    void Start()
    {
        
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
}
