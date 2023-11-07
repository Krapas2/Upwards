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

    private PlayerPowerupBroom playerPowerupBroom;

    void Start()
    {
        textBox = GetComponent<Text>();

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
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = UseBroomText;
        yield return WaitForPowerupUse();
        yield return new WaitForSeconds(timeBeforeChangingText);

        PlayerPrefs.SetInt("FinishedPowerupBroomTutorial", 1);
        textBox.text = "";
        Destroy(this);
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
