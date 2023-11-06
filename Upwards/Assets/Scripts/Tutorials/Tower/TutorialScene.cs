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

    private TutorialCloud tutorialCloud;
    private TutorialStructures tutorialStructures;

    void Start()
    {
        textBox = GetComponent<Text>();
        tutorialCloud = GetComponent<TutorialCloud>();
        tutorialStructures = GetComponent<TutorialStructures>();

        if(PlayerPrefs.GetInt("FinishedSceneTutorial") == 0){
            StartCoroutine(TutorialSequence());
        }else{
            Destroy(gameObject);
        }
    }

    IEnumerator TutorialSequence(){
        textBox.text = "";
        yield return WaitForFinishTutorials();
        yield return new WaitForSeconds(timeBeforeShowing);

        textBox.text = MoveScenesText;
        yield return new WaitForSeconds(timeBeforeHiding);

        PlayerPrefs.SetInt("FinishedSceneTutorial", 1);
        Destroy(gameObject);
    }

    IEnumerator WaitForFinishTutorials(){
        while(tutorialCloud || tutorialStructures){
            yield return null;
        }
        yield return null;
    }
}
