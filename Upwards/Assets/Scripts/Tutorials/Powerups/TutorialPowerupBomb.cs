using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPowerupBomb : MonoBehaviour
{

    public float timeBeforeChangingText = .25f;
    [TextAreaAttribute]
    public string UseBombText;
    [TextAreaAttribute]
    public string BombStickText;
    [TextAreaAttribute]
    public string BombAmountText;

    private Text textBox;

    [HideInInspector]
    public bool inProgress;

    private TutorialPowerupBroom tutorialPowerupBroom;
    private TutorialPowerupCloud tutorialPowerupCloud;

    private PlayerPowerupBomb playerPowerupBomb;

    void Start()
    {
        textBox = GetComponent<Text>();
        tutorialPowerupBroom = GetComponent<TutorialPowerupBroom>();
        tutorialPowerupCloud = GetComponent<TutorialPowerupCloud>();

        playerPowerupBomb = FindObjectOfType<PlayerPowerupBomb>();

        if(PlayerPrefs.GetInt("FinishedPowerupBombTutorial") == 0){
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
        textBox.text = UseBombText;
        yield return WaitForPowerupUse();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = BombStickText;
        PlayerPrefs.SetInt("FinishedPowerupBombTutorial", 1);
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = BombAmountText;
        yield return new WaitForSeconds(15f);

        textBox.text = "";
        inProgress = false;
        Destroy(this);
    }

    IEnumerator WaitForFinishTutorials(){
        while(BroomTutorialInProgress() || CloudTutorialInProgress()){
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

    bool CloudTutorialInProgress(){
        if(tutorialPowerupCloud){
            return tutorialPowerupCloud.inProgress;
        } else{
            return false;
        }
    }

    IEnumerator WaitForPowerupEnabled(){
        while(PlayerPrefs.GetInt("RockPowerupEnabled") != 1){
            yield return null;
        }
        yield return null;
    }

    IEnumerator WaitForPowerupUse(){
        while(!Input.GetButton("Bomb")){
            yield return null;
        }
        yield return null;
    }
}
