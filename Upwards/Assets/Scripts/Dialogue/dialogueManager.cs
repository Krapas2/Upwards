using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueBox{
    public GameObject gameobj;
    public int actorId;
}
public class dialogueManager : MonoBehaviour
{
    private TextMeshPro messageText;
    private GameObject player,npc;
    private int actorId;
    public GameObject prefabBox;
    Message[] currentMessages;

    List<DialogueBox> dialogueBoxes = new List<DialogueBox>();
    int activeMessage,activeBox = 0;
    public static bool isActive = false;
    private bool firstTime = true;


    public void ClearBoxes(){
        for (int i = 0; i < dialogueBoxes.Count; i++){
            Destroy(dialogueBoxes[i].gameobj);
        }
        dialogueBoxes.Clear();
    }
    public void OpenDialogue(Message[] messages, int npcId){
        currentMessages = messages;
        activeMessage = 0;
        activeBox = 0;
        isActive = true;
        actorId = npcId;
        npc = GameObject.Find("NPC " + actorId);
        player = GameObject.Find("Player");

        Debug.Log("Start conversation ! " + messages.Length);
        DisplayMessage();
    }

    void DisplayMessage(){

        DialogueBox dgBoxTemp = new DialogueBox();
        //ADICIONA PRIMEIRA CAIXA DE TEXTO NO ARRAY
        if(firstTime){
            dgBoxTemp.gameobj = Instantiate(prefabBox,
                                             new Vector3(0,0,0), 
                                             Quaternion.identity);
            firstTime = false;
        }else{
            dgBoxTemp.gameobj = Instantiate(dialogueBoxes[activeBox].gameobj,
                                             new Vector3(0,0,0), 
                                             Quaternion.identity);
            activeBox++; 
        }

        dgBoxTemp.actorId = currentMessages[activeBox].actorId;

        Debug.Log(dgBoxTemp.actorId);
        dialogueBoxes.Add(dgBoxTemp);  

        dialogueBoxes[activeBox].gameobj.name = "Box " + activeBox.ToString();                                 

        messageText = dialogueBoxes[activeBox].gameobj.GetComponentsInChildren<TextMeshPro>()[0];
        
        Message messagueToDisplay = currentMessages[activeBox];
        messageText.text = messagueToDisplay.message;    
    }

    public void NextMessage(){
        activeMessage++;
        if (activeMessage < currentMessages.Length){
            DisplayMessage();
        }else{
            Debug.Log("Conversation End");
            ClearBoxes();
            isActive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && isActive){
            NextMessage();
        }

        if(prefabBox != null){
            int aux = dialogueBoxes.Count-1;
            for (int i = 0; i < dialogueBoxes.Count; i++)
            {
                if(dialogueBoxes[aux].actorId != 1){
                    dialogueBoxes[aux].gameobj.transform.position = new Vector3(npc.transform.position.x, 
                                                                              npc.transform.position.y+((i+1)*3.5f),
                                                                              npc.transform.position.z);
                    aux--;                                                        
                }
                else{
                    dialogueBoxes[aux].gameobj.transform.position = new Vector3(player.transform.position.x, 
                                                                              player.transform.position.y+((i+1)*3.5f),
                                                                              player.transform.position.z);
                    aux--;                                                          
                }
            }
        }
    }
}