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
public class DialogueManager : MonoBehaviour
{
    private TextMeshPro messageText;
    private GameObject player,npc;
    private int actorId;
    public float spaceBox;
    private string powerUpName;
    public GameObject prefabBox;
    Message[] currentMessages;
    List<DialogueBox> dialogueBoxes = new List<DialogueBox>();
    int activeMessage,activeBox = 0;
    public static bool isActive = false;
    private bool firstTime = true;
    private bool isFinal;

    void Start(){
        //GET GAMEOBJECTS
        player = GameObject.Find("Player");
    }
    public void EnablePowerUp(int actorId){
        switch (actorId)
        {
            //NUVEM
            case 1:
                player.GetComponent<PlayerPowerupCloud>().enabled = true;
                break;

            //ARVORE
            case 2:
                player.GetComponent<PlayerPowerupBroom>().enabled = true;
                break;
            
            //PEDRA
            case 3:
                player.GetComponent<PlayerPowerupBomb>().enabled = true;
                break;
            default:
                player.GetComponent<PlayerPowerupBroom>().enabled = true;
                break;
        }
    }
    public void ClearBoxes(){
        for (int i = 0; i < dialogueBoxes.Count; i++){
            Destroy(dialogueBoxes[i].gameobj);
        }
        dialogueBoxes.Clear();
        
        //RESETANDO VARS
        isActive = false;
        firstTime = true;
    }
    public void OpenDialogue(Message[] messages, int npcId, bool isFinalDialogue){
        currentMessages = messages;
        activeMessage = 0;
        activeBox = 0;
        isActive = true;
        actorId = npcId;
        isFinal = isFinalDialogue;

        //GET GAMEOBJECT BY NPC ID
        npc = GameObject.Find("NPC " + actorId);

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

        dgBoxTemp.actorId = currentMessages[activeBox].actorId; //eerroo

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

            //ATIVA SCRIPT PARA ADICIONAR POWERUP
            if(isFinal)
                EnablePowerUp(actorId);

            ClearBoxes();
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
                //ATOR DO PLAYER SEMPRE É ZERO
                if(dialogueBoxes[aux].actorId != 0){
                    dialogueBoxes[aux].gameobj.transform.position = new Vector3(npc.transform.position.x, 
                                                                                npc.transform.position.y+((i+1)*spaceBox),
                                                                                npc.transform.position.z);
                    aux--;                                                        
                }
                else{
                    dialogueBoxes[aux].gameobj.transform.position = new Vector3(player.transform.position.x, 
                                                                                player.transform.position.y+((i+1)*spaceBox),
                                                                                player.transform.position.z);
                    aux--;                                                          
                }
            }
        }
    }
}