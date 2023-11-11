using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialResource : MonoBehaviour
{
    public float timeBeforeChangingText = .25f;
    [TextAreaAttribute]
    public string breakResourceText;
    [TextAreaAttribute]
    public string collectResourceText;

    private Text textBox;
    private int itemsInScene;

    void Start()
    {
        textBox = GetComponent<Text>();

        if(PlayerPrefs.GetInt("FinishedResourceTutorial") == 0){
            StartCoroutine(TutorialSequence());
        }else{
            Destroy(this);
        }
    }

    IEnumerator TutorialSequence(){
        textBox.text = "";
        yield return new WaitForSeconds(5f);

        textBox.text = breakResourceText;
        yield return WaitForBreakResource();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = collectResourceText;
        yield return WaitForCollectResource();
        yield return new WaitForSeconds(timeBeforeChangingText);

        PlayerPrefs.SetInt("FinishedResourceTutorial", 1);
        textBox.text = "";
        Destroy(this);
    }

    IEnumerator WaitForBreakResource(){
        while(!FindObjectOfType<ItemDrop>()){
            yield return null;
        }
        yield return null;
    }
    IEnumerator WaitForCollectResource(){
        while((FindObjectsOfType<ItemDrop>().Length >= itemsInScene)){
            itemsInScene = FindObjectsOfType<ItemDrop>().Length;
            yield return null;
        }
        yield return null;
    }
}
