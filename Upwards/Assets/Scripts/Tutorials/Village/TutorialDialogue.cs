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
    private TutorialMovement tutorialMovement;
    void Start()
    {
        textBox = GetComponent<Text>();

        tutorialMovement = GetComponent<TutorialMovement>();

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
        yield return WaitForFinishTutorials();
        yield return WaitForDialogueBox();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = skipDialogueText;
        yield return WaitForDialogueSkip();
        yield return new WaitForSeconds(timeBeforeChangingText);

        PlayerPrefs.SetInt("FinishedDialogueTutorial", 1);
        textBox.text = "";
        Destroy(this);
    }

    IEnumerator WaitForFinishTutorials()
    {
        while (tutorialMovement)
        {
            yield return null;
        }
        yield return null;
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
