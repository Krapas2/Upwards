using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialogue : MonoBehaviour
{
    public float timeBeforeChangingText = .25f;
    [TextAreaAttribute]
    public string skipDialogueText;

    private Text textBox;
    void Start()
    {
        textBox = GetComponent<Text>();

        if (PlayerPrefs.GetInt("FinishedDialogueTutorial") == 0)
        {
            StartCoroutine(TutorialSequence());
        }
        else
        {
            Destroy(this);
        }
    }

    IEnumerator TutorialSequence()
    {
        textBox.text = "";
        yield return WaitForDialogueBox();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = skipDialogueText;
        yield return WaitForDialogueSkip();
        yield return new WaitForSeconds(timeBeforeChangingText);

        PlayerPrefs.SetInt("FinishedDialogueTutorial", 1);
        textBox.text = "";
        Destroy(this);
    }

    IEnumerator WaitForDialogueBox()
    {
        while (!GameObject.FindGameObjectWithTag("DialogueBox"))
        {
            yield return null;
        }
        yield return null;
    }

    IEnumerator WaitForDialogueSkip()
    {
        while (!Input.GetButton("Next Dialogue"))
        {
            yield return null;
        }
        yield return null;
    }
}
