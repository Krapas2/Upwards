using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueNpc : MonoBehaviour
{
    public DialogueTrigger trigger;
    public Transform dialogueCheck;
    public LayerMask NPCLayerMask;
    private PlayerInventory inventory;
    private PlayerPowerupManager powerupManager;

    [HideInInspector]
    public bool NPCTriggered;

    [HideInInspector]
    public bool triggeredFirstDialogue = false;

    [HideInInspector]
    public bool triggeredLastDialogue = false;
    
    bool CheckLastDialogue(int npcId){
        int itemIndex, powerupIndex;
        switch (npcId)
        {
            case 1:
                itemIndex = inventory.ItemIndexFromName("Cloud");
                powerupIndex = powerupManager.PowerupIndexFromItemName("Cloud");
            break;
            case 2:
                itemIndex = inventory.ItemIndexFromName("Wood");
                powerupIndex = powerupManager.PowerupIndexFromItemName("Wood");
            break;
            case 3:
                itemIndex = inventory.ItemIndexFromName("Rock");
                powerupIndex = powerupManager.PowerupIndexFromItemName("Rock");
            break;
            default:
                itemIndex = inventory.ItemIndexFromName("Cloud");
                powerupIndex = powerupManager.PowerupIndexFromItemName("Cloud");
            break;
        }
        int itemAmount = inventory.items[itemIndex].amount;
        int itemMax = powerupManager.powerups[powerupIndex].ItemRequiredAmount;

        Debug.Log("Item qtde: "+itemAmount);
        Debug.Log("Item total: "+itemMax);
        Debug.Log("NPC: "+npcId);

        if (itemAmount >= itemMax){
            inventory.items[itemIndex].amount = itemAmount - itemMax;
            return true;
        }
        else
            return false;
    }
    void Start(){      
        inventory = FindObjectOfType<PlayerInventory>();
        powerupManager = FindObjectOfType<PlayerPowerupManager>();
    }
    void Update(){
        NPCTriggered = Physics2D.OverlapCircle(dialogueCheck.position, .4f, NPCLayerMask);
        
        if (NPCTriggered && !triggeredFirstDialogue){   
            trigger.StartDialogue();
            triggeredFirstDialogue = true;
        }
        

        if (NPCTriggered && !triggeredLastDialogue && CheckLastDialogue(trigger.npcId)){   
            trigger.StartFinalDialogue();
            triggeredLastDialogue = true;
        }
    }
}
