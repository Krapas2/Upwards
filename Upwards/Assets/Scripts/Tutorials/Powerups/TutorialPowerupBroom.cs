using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPowerupBroom : MonoBehaviour
{

    public float timeBeforeChangingText = .25f;
    [TextAreaAttribute]
    public string UseBroomText;

    private Text textBox;

    [HideInInspector]
    public bool inProgress;
    
    private TutorialPowerupBomb tutorialPowerupBomb;
    private TutorialPowerupCloud tutorialPowerupCloud;

    private PlayerPowerupBroom playerPowerupBroom;

    void Start()
    {
        textBox = GetComponent<Text>();
        tutorialPowerupBomb = GetComponent<TutorialPowerupBomb>();
        tutorialPowerupCloud = GetComponent<TutorialPowerupCloud>();

        playerPowerupBroom = FindObjectOfType<PlayerPowerupBroom>();

        if(PlayerPrefs.GetInt("FinishedPowerupBroomTutorial") == 0){
            StartCoroutine(TutorialSequence());
        }else{
            textBox.text = "";
            Destroy(this);
        }
    }

    IEnumerator TutorialSequence(){
        textBox.text = "";
        yield return WaitForPowerupEnabled();
        yield return WaitForFinishTutorials();
        yield return new WaitForSeconds(timeBeforeChangingText);

        inProgress = true;
        textBox.text = UseBroomText;
        yield return WaitForPowerupUse();
        yield return new WaitForSeconds(timeBeforeChangingText);

        PlayerPrefs.SetInt("FinishedPowerupBroomTutorial", 1);
        textBox.text = "";
        inProgress = false;
        Destroy(this);
    }

    IEnumerator WaitForFinishTutorials(){
        while(BombTutorialInProgress() || CloudTutorialInProgress()){
            yield return null;
        }
        yield return null;
    }

    bool BombTutorialInProgress(){
        if(tutorialPowerupBomb){
            return tutorialPowerupBomb.inProgress;
        } else{
            return false;
        }
    }

    bool CloudTutorialInProgress(){
        if(tutorialPowerupCloud){
            return tutorialPowerupCloud.inProgress;
        } else{
            return false;
        }
    }

    IEnumerator WaitForPowerupEnabled(){
        while(playerPowerupBroom.enabled == false){
            yield return null;
        }
        yield return null;
    }

    IEnumerator WaitForPowerupUse(){
        while(!Input.GetButton("Broom")){
            yield return null;
        }
        yield return null;
    }
}
