using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPowerupCloud : MonoBehaviour
{

    public float timeBeforeChangingText = .25f;
    [TextAreaAttribute]
    public string UseCloudText;

    private Text textBox;
    
    [HideInInspector]
    public bool inProgress;

    private TutorialPowerupBroom tutorialPowerupBroom;
    private TutorialPowerupBomb tutorialPowerupBomb;

    private PlayerPowerupCloud playerPowerupCloud;

    void Start()
    {
        textBox = GetComponent<Text>();
        tutorialPowerupBroom = GetComponent<TutorialPowerupBroom>();
        tutorialPowerupBomb = GetComponent<TutorialPowerupBomb>();

        playerPowerupCloud = FindObjectOfType<PlayerPowerupCloud>();

        if(PlayerPrefs.GetInt("FinishedPowerupCloudTutorial") == 0){
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
        textBox.text = UseCloudText;
        yield return WaitForPowerupUse();
        yield return new WaitForSeconds(timeBeforeChangingText);

        PlayerPrefs.SetInt("FinishedPowerupCloudTutorial", 1);
        textBox.text = "";
        inProgress = false;
        Destroy(this);
    }

    IEnumerator WaitForFinishTutorials(){
        while(BroomTutorialInProgress() || BombTutorialInProgress()){
            yield return null;
        }
        yield return null;
    }

    bool BroomTutorialInProgress(){
        if(tutorialPowerupBroom){
            return tutorialPowerupBroom.inProgress;
        } else{
            return false;
        }
    }

    bool BombTutorialInProgress(){
        if(tutorialPowerupBomb){
            return tutorialPowerupBomb.inProgress;
        } else{
            return false;
        }
    }

    IEnumerator WaitForPowerupEnabled(){
        while(PlayerPrefs.GetInt("CloudPowerupEnabled") != 1){
            yield return null;
        }
        yield return null;
    }

    
    IEnumerator WaitForPowerupUse(){
        while(!Input.GetButton("Cloud")){
            yield return null;
        }
        yield return null;
    }
}
