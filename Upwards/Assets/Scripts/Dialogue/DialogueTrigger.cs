using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] initialMessages;
    public Message[] finalMessages;
    public int npcId;
    public void StartDialogue(){
        FindObjectOfType<DialogueManager>().OpenDialogue(initialMessages,npcId,false);
    }
    public void StartFinalDialogue(){
        FindObjectOfType<DialogueManager>().OpenDialogue(finalMessages,npcId,true);
    }
}

[System.Serializable]
public class Message {
    public int actorId;
    public string message;
}
