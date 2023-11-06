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
    
    bool CheckLastDialogue(string itemId){
        int itemIndex, powerupIndex;

        itemIndex = inventory.ItemIndexFromName(itemId);
        powerupIndex = powerupManager.PowerupIndexFromItemName(itemId);
        
        int itemAmount = inventory.items[itemIndex].amount;
        int itemMax = powerupManager.powerups[powerupIndex].ItemRequiredAmount;

       /* Debug.Log("Item qtde: " + itemAmount);
        Debug.Log("Item total: " + itemMax);
        Debug.Log("NPC: " + itemId); */

        if (itemAmount >= itemMax){
            inventory.items[itemIndex].amount = itemAmount - itemMax;
            return true;
        }
        else
            return false;
    }
    void Start(){      
        triggeredFirstDialogue = false;
        triggeredLastDialogue = false;
        inventory = FindObjectOfType<PlayerInventory>();
        powerupManager = FindObjectOfType<PlayerPowerupManager>();
    }
    void Update(){
        NPCTriggered = Physics2D.OverlapCircle(dialogueCheck.position, 1.5f, NPCLayerMask);
        
        if (NPCTriggered && !triggeredFirstDialogue){   
            triggeredFirstDialogue = true;
            trigger.StartDialogue();
        }
        

        if (NPCTriggered && !triggeredLastDialogue && CheckLastDialogue(trigger.itemId)){   
            triggeredLastDialogue = true;
            trigger.StartFinalDialogue();
        }
    }
}
