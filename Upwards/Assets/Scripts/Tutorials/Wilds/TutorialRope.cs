using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialRope : MonoBehaviour
{
    public float depthToActivate = 50;
    public float timeBeforeChangingText = .25f;
    [TextAreaAttribute]
    public string throwRopeText;
    [TextAreaAttribute]
    public string climbRopeText;

    private Text textBox;
    private TutorialResource tutorialResource;
    private Transform player;

    void Start()
    {
        textBox = GetComponent<Text>();
        tutorialResource = GetComponent<TutorialResource>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        if(PlayerPrefs.GetInt("FinishedRopeTutorial") == 0){
            StartCoroutine(TutorialSequence());
        }else{
            Destroy(this);
        }
    }

    IEnumerator TutorialSequence(){
        textBox.text = "";
        yield return WaitForFinishTutorials();
        yield return WaitForPlayerReachHeight();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = throwRopeText;
        yield return WaitForThrowRope();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = climbRopeText;
        yield return WaitForClimbRope();
        yield return new WaitForSeconds(timeBeforeChangingText);

        PlayerPrefs.SetInt("FinishedRopeTutorial", 1);
        textBox.text = "";
        Destroy(this);
    }

    IEnumerator WaitForFinishTutorials(){
        while(tutorialResource){
            yield return null;
        }
        yield return null;
    }

    IEnumerator WaitForPlayerReachHeight(){
        while(player.position.y > depthToActivate){
            yield return null;
        }
        yield return null;
    }

    IEnumerator WaitForThrowRope(){
        while(!Input.GetButton("Fire2")){
            yield return null;
        }
        yield return null;
    }

    IEnumerator WaitForClimbRope(){
        while(Input.GetAxis("Vertical") == 0){
            yield return null;
        }
        yield return null;
    }
}
