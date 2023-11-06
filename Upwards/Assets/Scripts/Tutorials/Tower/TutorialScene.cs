using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScene : MonoBehaviour
{
    public float timeBeforeShowing = 15f;
    public float timeBeforeHiding = .25f;
    [TextAreaAttribute]
    public string MoveScenesText;

    private Text textBox;

    private TutorialStructures tutorialStructures;
    private TutorialCloud tutorialCloud;
    private TutorialTrackers tutorialTrackers;

    void Start()
    {
        textBox = GetComponent<Text>();
        tutorialStructures = GetComponent<TutorialStructures>();
        tutorialCloud = GetComponent<TutorialCloud>();
        tutorialTrackers = GetComponent<TutorialTrackers>();

        if(PlayerPrefs.GetInt("FinishedSceneTutorial") == 0){
            StartCoroutine(TutorialSequence());
        }else{
            textBox.text = "";
            Destroy(this);
        }
    }

    IEnumerator TutorialSequence(){
        textBox.text = "";
        yield return WaitForFinishTutorials();
        yield return new WaitForSeconds(timeBeforeShowing);

        textBox.text = MoveScenesText;
        yield return new WaitForSeconds(timeBeforeHiding);

        PlayerPrefs.SetInt("FinishedSceneTutorial", 1);
        textBox.text = "";
        Destroy(this);
    }

    IEnumerator WaitForFinishTutorials(){
        while(tutorialStructures || tutorialCloud || tutorialTrackers){
            yield return null;
        }
        yield return null;
    }
}
