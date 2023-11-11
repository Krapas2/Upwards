using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerupManager : MonoBehaviour
{
    [System.Serializable]
    public struct Powerup
    {
        public MonoBehaviour powerup;
        public MonoBehaviour meter;
        public string ItemRequiredName;
        public int ItemRequiredAmount;
    }

    public Powerup[] powerups;

    private PlayerInventory playerInventory;
    private PlayerBreakTile playerBreakTile;
    private PlayerBreakSprite playerBreakSprite;

    void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
        playerBreakTile = GetComponent<PlayerBreakTile>();
        playerBreakSprite = GetComponent<PlayerBreakSprite>();

        float tileBreakTimeChange = playerBreakSprite.breakTime * (.9f / powerups.Length);
        float spriteBreakTimeChange = playerBreakTile.breakTime * (.9f / powerups.Length);
        
        foreach(Powerup powerup in powerups){
            bool enabled = PlayerPrefs.GetInt(powerup.ItemRequiredName + "PowerupEnabled") != 0;
            powerup.powerup.enabled = enabled;
            powerup.meter.gameObject.SetActive(enabled);
            if(enabled){
                playerBreakSprite.breakTime -= tileBreakTimeChange;
                playerBreakTile.breakTime -= spriteBreakTimeChange;
            }
        }
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

    public bool HasAllPowerups()
    {
        bool output = true;
        foreach (Powerup powerup in powerups)
        {
            output &= powerup.powerup.enabled;
        }
        return output;
    }
}
