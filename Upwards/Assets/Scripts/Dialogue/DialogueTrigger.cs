using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] initialMessages;
    public Message[] finalMessages;
    public string itemId;
    public Color32 colorDg;
    public void StartDialogue(){
        FindObjectOfType<DialogueManager>().OpenDialogue(initialMessages,
                                                         itemId,
                                                         false,
                                                         gameObject,
                                                         colorDg);
    }
    public void StartFinalDialogue(){
        FindObjectOfType<DialogueManager>().OpenDialogue(finalMessages,
                                                         itemId,
                                                         true,
                                                         gameObject,
                                                         colorDg);
    }
}

[System.Serializable]
public class Message {
    public int actorId;
    public string message;
}
