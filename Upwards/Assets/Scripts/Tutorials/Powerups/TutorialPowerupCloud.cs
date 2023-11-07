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
        yield return WaitForFinishTutorials();
        yield return WaitForPowerupEnabled();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = UseCloudText;
        yield return WaitForPowerupUse();
        yield return new WaitForSeconds(timeBeforeChangingText);

        PlayerPrefs.SetInt("FinishedPowerupCloudTutorial", 1);
        textBox.text = "";
        Destroy(this);
    }

    IEnumerator WaitForFinishTutorials(){
        while(tutorialPowerupBroom || tutorialPowerupBroom){
            yield return null;
        }
        yield return null;
    }

    IEnumerator WaitForPowerupEnabled(){
        while(playerPowerupCloud.enabled == false){
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
