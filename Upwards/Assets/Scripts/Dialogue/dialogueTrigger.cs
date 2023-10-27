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
        for (int i = 0; i < messages.Length; i++)
        {
            Debug.Log(messages[i].message);
        }
        FindObjectOfType<DialogueManager>().OpenDialogue(messages,npcId,powerUp);
    }
}

[System.Serializable]
public class Message {
    public int actorId;
    public string message;
}
