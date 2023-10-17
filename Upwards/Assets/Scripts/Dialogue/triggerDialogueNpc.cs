using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueNpc : MonoBehaviour
{
    public DialogueTrigger trigger;
    public Transform dialogueCheck;
    public LayerMask NPC;

    [HideInInspector]
    public bool NPCTriggered;
    private bool triggered = false;
    void Update(){

        NPCTriggered = Physics2D.OverlapCircle(dialogueCheck.position, .2f, NPC);
        if (NPCTriggered && !triggered){   
            trigger.StartDialogue();
            triggered = true;
        }
    }
}
