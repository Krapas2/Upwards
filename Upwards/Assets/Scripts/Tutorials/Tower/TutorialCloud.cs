using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCloud : MonoBehaviour
{
    public float heightToActivate = 30;
    public float timeBeforeChangingText = .25f;
    [TextAreaAttribute]
    public string breakCloudText;
    [TextAreaAttribute]
    public string collectCloudText;

    private Text textBox;
    private Transform player;

    void Start()
    {
        textBox = GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if(PlayerPrefs.GetInt("FinishedCloudTutorial") == 0){
            StartCoroutine(TutorialSequence());
        }else{
            Destroy(this);
        }
    }

    IEnumerator TutorialSequence(){
        textBox.text = "";
        yield return WaitForPlayerReachHeight();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = breakCloudText;
        yield return WaitForBreakCloud();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = collectCloudText;
        yield return WaitForCollectCloud();
        yield return new WaitForSeconds(timeBeforeChangingText);

        PlayerPrefs.SetInt("FinishedCloudTutorial", 1);
        Destroy(gameObject);
    }

    IEnumerator WaitForPlayerReachHeight(){
        while(player.position.y < heightToActivate){
            yield return null;
        }
        yield return null;
    }

    IEnumerator WaitForBreakCloud(){
        while(!FindObjectOfType<ItemDrop>()){
            yield return null;
        }
        yield return null;
    }
    IEnumerator WaitForCollectCloud(){
        while(FindObjectOfType<ItemDrop>()){
            yield return null;
        }
        yield return null;
    }
}
