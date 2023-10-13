using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerDialogueNpc : MonoBehaviour
{
    public dialogueTrigger trigger;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision){

        if (collision.gameObject.CompareTag("Player") && !triggered){   
            trigger.StartDialogue();
            triggered = true;
        }
    }
}
