using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMovement : MonoBehaviour
{
    public float timeBeforeChangingText = .25f;
    [TextAreaAttribute]
    public string horizontalMovementText;
    [TextAreaAttribute]
    public string JumpText;

    private Text textBox;
    void Start()
    {
        textBox = GetComponent<Text>();

        if (PlayerPrefs.GetInt("FinishedMovementTutorial") == 0)
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
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = horizontalMovementText;
        yield return WaitForMovement();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = JumpText;
        yield return WaitForJump();
        yield return new WaitForSeconds(timeBeforeChangingText);

        PlayerPrefs.SetInt("FinishedMovementTutorial", 1);
        textBox.text = "";
        Destroy(this);
    }

    IEnumerator WaitForMovement()
    {
        while (Input.GetAxisRaw("Horizontal") == 0)
        {
            yield return null;
        }
        yield return null;
    }
    
    IEnumerator WaitForJump()
    {
        while (!Input.GetButton("Jump"))
        {
            yield return null;
        }
        yield return null;
    }
}
