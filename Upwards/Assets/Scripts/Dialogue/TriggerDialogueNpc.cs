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
    private int itemIndex, powerupIndex, itemAmount, itemMax;
    
    bool CheckLastDialogue(string itemId){

        //GET INDEX AMOUNT E ITEM REQUIRED
        itemIndex = inventory.ItemIndexFromName(itemId);
        powerupIndex = powerupManager.PowerupIndexFromItemName(itemId);
        
        //GET QTDE AMOUNT E QTDE ITEMMAX
        itemAmount = inventory.items[itemIndex].amount;
        itemMax = powerupManager.powerups[powerupIndex].ItemRequiredAmount;

       /* Debug.Log("Item qtde: " + itemAmount);
        Debug.Log("Item total: " + itemMax);
        Debug.Log("NPC: " + itemId); */

        if (itemAmount >= itemMax)
            return true;
        else
            return false;
    }
    void Start(){      
        inventory = FindObjectOfType<PlayerInventory>();
        powerupManager = FindObjectOfType<PlayerPowerupManager>();
    }
    void Update(){
        NPCTriggered = Physics2D.OverlapCircle(dialogueCheck.position, 1.5f, NPCLayerMask);

        if (NPCTriggered && 
            PlayerPrefs.GetInt(trigger.itemId + "triggeredFirstDialogue") == 0){
            
            //SETTANDO PLAYER PREF DE TRIGGER DO PRIMEIRO DIALOGO
            PlayerPrefs.SetInt(trigger.itemId + "triggeredFirstDialogue",1);
            
            trigger.StartDialogue();
        }
        

        if (NPCTriggered && 
            PlayerPrefs.GetInt(trigger.itemId + "triggeredLastDialogue") == 0 && 
            CheckLastDialogue(trigger.itemId)){   

            //SETTANDO PLAYER PREF DE TRIGGER DO ULTIMO DIALOGO
            PlayerPrefs.SetInt(trigger.itemId + "triggeredLastDialogue",1);

            //REMOVE QTDE DE ITEM APÓS TRIGGER
            inventory.items[itemIndex].amount = itemAmount - itemMax;

            trigger.StartFinalDialogue();
        }
    }
}
