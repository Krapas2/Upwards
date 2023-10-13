using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class dialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public int npcId;

    public void StartDialogue(){
        FindObjectOfType<dialogueManager>().OpenDialogue(messages,npcId);
    }
}

[System.Serializable]
public class Message {
    public int actorId;
    public string message;
}
