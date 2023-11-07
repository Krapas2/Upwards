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

    private Text textBox;

    private TutorialPowerupBroom tutorialPowerupBroom;

    private PlayerPowerupBomb playerPowerupBomb;

    void Start()
    {
        textBox = GetComponent<Text>();
        tutorialPowerupBroom = GetComponent<TutorialPowerupBroom>();

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
        yield return WaitForFinishTutorials();
        yield return WaitForPowerupEnabled();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = UseBombText;
        yield return WaitForPowerupUse();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = BombStickText;
        PlayerPrefs.SetInt("FinishedPowerupBombTutorial", 1);
        yield return new WaitForSeconds(5f);

        textBox.text = "";
        Destroy(this);
    }

    IEnumerator WaitForFinishTutorials(){
        while(tutorialPowerupBroom){
            yield return null;
        }
        yield return null;
    }

    IEnumerator WaitForPowerupEnabled(){
        while(playerPowerupBomb.enabled == false){
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
