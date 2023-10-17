using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public int npcId;
    public string powerUp;
    public void StartDialogue(){
        FindObjectOfType<DialogueManager>().OpenDialogue(messages,npcId,powerUp);
    }
}

[System.Serializable]
public class Message {
    public int actorId;
    public string message;
}
