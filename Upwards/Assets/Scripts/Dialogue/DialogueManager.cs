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
    private GameObject player,npc;
    private TriggerDialogueNpc triggerNPC;
    private string itemRequired;
    public float spaceBox;
    [SerializeField] private float typingSpeed = 0.04f;
    public GameObject prefabBox;
    Message[] currentMessages;
    List<DialogueBox> dialogueBoxes = new List<DialogueBox>();
    int activeMessage,activeBox = 0;
    Color32 colorDialogue;
    public static bool isActive = false;
    private bool firstTime = true;
    private bool isFinal;

    void Start(){
        //GET GAMEOBJECTS
        player = GameObject.FindWithTag("Player");
    }

    private IEnumerator DisplayLine(string line){
        //empty dialogue text
        messageText.text = "";

        isActive = false;
        foreach (char letter in line.ToCharArray())
        {
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
        isActive = false;
        firstTime = true;
    }
    public void OpenDialogue(Message[] messages, 
                             string itemId, 
                             bool isFinalDialogue,
                             GameObject actorObject,
                             Color32 colorDg){
        
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
        isActive = true;
        itemRequired = itemId;
        isFinal = isFinalDialogue;
        npc = actorObject;
        colorDialogue = colorDg;

        Debug.Log("Start conversation ! " + messages.Length);
        DisplayMessage();
    }

    void DisplayMessage(){

        DialogueBox dgBoxTemp = new DialogueBox();
        /*Vector3 vDialogue;

        if (currentMessages[activeBox].actorId != 0){
            vDialogue = new Vector3(npc.transform.position.x, 
                                    npc.transform.position.y+((activeBox+1)*spaceBox),
                                    npc.transform.position.z);
        }else{
            vDialogue = new Vector3(player.transform.position.x, 
                                    player.transform.position.y+((activeBox+1)*spaceBox),
                                    player.transform.position.z);
        } */
        
        //ADICIONA PRIMEIRA CAIXA DE TEXTO NO ARRAY
        if(firstTime){
            dgBoxTemp.gameobj = Instantiate(prefabBox,
                                            new Vector3(0,0,0), 
                                            Quaternion.identity,
                                            GameObject.Find("Canvas").transform);
            firstTime = false;
        }else{
            dgBoxTemp.gameobj = Instantiate(dialogueBoxes[activeBox].gameobj,
                                            new Vector3(0,0,0),
                                            Quaternion.identity,
                                            GameObject.Find("Canvas").transform);
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