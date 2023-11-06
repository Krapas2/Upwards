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
    private TextMeshProUGUI messageText;
    private GameObject player,npc,canvas;
    private TriggerDialogueNpc triggerNPC;
    private string itemRequired;
    public float spaceBox;
    [SerializeField] private float typingSpeed = 0.04f;
    public GameObject prefabBox;
    Message[] currentMessages;
    List<DialogueBox> dialogueBoxes = new List<DialogueBox>();
    int activeMessage,activeBox = 0;
    Color32 colorDialogue;
    public static bool isActive = true;
    private bool firstTime = true;
    private bool isFinal;

    void Start(){
        //GET GAMEOBJECTS
        player = GameObject.FindWithTag("Player");
        canvas = FindObjectOfType<Canvas>().gameObject;
    }

    private IEnumerator DisplayLine(string line){
        //empty dialogue text
        messageText.text = "";
        
        isActive = false;

        foreach (char letter in line.ToCharArray()){
            Debug.Log(isActive);
            messageText.text += letter;
            
            yield return new WaitForSeconds(typingSpeed);
        }

        isActive = true;
    }

    public void ClearBoxes(){
        for (int i = 0; i < dialogueBoxes.Count; i++){
            Destroy(dialogueBoxes[i].gameobj);
        }
        dialogueBoxes.Clear();
        
        //RESETANDO VARS
        firstTime = true;
        isActive = true;
    }
    public void OpenDialogue(Message[] messages, 
                             string itemId, 
                             bool isFinalDialogue,
                             GameObject actorObject,
                             Color32 colorDg){
        if (isActive){    
            //SE AINDA TIVER DIÁLOGO RESETAR TRIGGER DO DIÁLOGO
            if (dialogueBoxes.Count > 0){
                ClearBoxes();
                triggerNPC = npc.GetComponent<TriggerDialogueNpc>();

                if(!isFinalDialogue)
                    triggerNPC.triggeredFirstDialogue = false;   
                else 
                    triggerNPC.triggeredLastDialogue = false;   
            }
            
            currentMessages = messages;
            activeMessage = 0;
            activeBox = 0;
            itemRequired = itemId;
            isFinal = isFinalDialogue;
            npc = actorObject;
            colorDialogue = colorDg;

            Debug.Log("Start conversation ! " + messages.Length);
            
            DisplayMessage();
        } else {
            if(!isFinalDialogue)
                actorObject.GetComponent<TriggerDialogueNpc>().triggeredFirstDialogue = false;   
            else 
                actorObject.GetComponent<TriggerDialogueNpc>().triggeredLastDialogue = false;   
        }
    }

    void DisplayMessage(){

        DialogueBox dgBoxTemp = new DialogueBox();
        
        //ADICIONA PRIMEIRA CAIXA DE TEXTO NO ARRAY
        if(firstTime){
            dgBoxTemp.gameobj = Instantiate(prefabBox,
                                            new Vector3(0,0,0), 
                                            Quaternion.identity,
                                            canvas.transform);
            firstTime = false;
        }else{
            dgBoxTemp.gameobj = Instantiate(dialogueBoxes[activeBox].gameobj,
                                            new Vector3(0,0,0),
                                            Quaternion.identity,
                                            canvas.transform);
            activeBox++; 
        }

        dgBoxTemp.actorId = currentMessages[activeBox].actorId; 
        
        if (dgBoxTemp.actorId != 0)
            dgBoxTemp.gameobj.GetComponent<Image>().color = colorDialogue;
        else
            dgBoxTemp.gameobj.GetComponent<Image>().color = new Color32(239,110,16,255);
        
        dialogueBoxes.Add(dgBoxTemp);  

        dialogueBoxes[activeBox].gameobj.name = "Box " + activeBox.ToString();     
                                    
        messageText = dialogueBoxes[activeBox].gameobj.GetComponentsInChildren<TextMeshProUGUI>()[0];
        
        Message messagueToDisplay = currentMessages[activeBox];

        //escrevendo letra por letra
        StartCoroutine(DisplayLine(messagueToDisplay.message));
    }

    public void NextMessage(){
        activeMessage++;
        if (activeMessage < currentMessages.Length){
            DisplayMessage();
        }else{
            Debug.Log("Conversation End");

            //ATIVA PLAYER PREF PARA ADICIONAR POWERUP
            if(isFinal)         
                PlayerPrefs.SetInt(itemRequired + "PowerupEnabled",1);

            ClearBoxes();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Next Dialogue") && isActive){
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