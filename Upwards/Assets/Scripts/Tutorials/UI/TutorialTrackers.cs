using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrackers : MonoBehaviour
{
    public float timeBeforeChangingText = .25f;
    [TextAreaAttribute]
    public string showTrackerText;

    private Text textBox;
    private TutorialStructures tutorialStructures;
    private TutorialCloud tutorialCloud;
    void Start()
    {
        textBox = GetComponent<Text>();
        tutorialStructures = GetComponent<TutorialStructures>();
        tutorialCloud = GetComponent<TutorialCloud>();

        if(PlayerPrefs.GetInt("FinishedTrackerTutorial") == 0){
            StartCoroutine(TutorialSequence());
        }else{
            Destroy(this);
        }
    }

    IEnumerator TutorialSequence(){
        textBox.text = "";
        yield return WaitForFinishTutorials();
        yield return WaitForItemDrop();
        yield return WaitForItemCollect();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = showTrackerText;
        yield return WaitForShowTrackers();
        yield return new WaitForSeconds(timeBeforeChangingText);

        PlayerPrefs.SetInt("FinishedTrackerTutorial", 1);
        textBox.text = "";
        Destroy(this);
    }

    IEnumerator WaitForFinishTutorials(){
        while(tutorialCloud || tutorialStructures){
            yield return null;
        }
        yield return null;
    }

    IEnumerator WaitForItemDrop(){
        while(!FindObjectOfType<ItemDrop>()){
            yield return null;
        }
        yield return null;
    }

    IEnumerator WaitForItemCollect(){
        while(FindObjectOfType<ItemDrop>()){
            yield return null;
        }
        yield return null;
    }

    IEnumerator WaitForShowTrackers(){
        while(!Input.GetButton("ShowUI")){
            yield return null;
        }
        yield return null;
    }
}
