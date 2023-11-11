using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStructures : MonoBehaviour
{
    public float timeBeforeChangingText = .25f;
    [TextAreaAttribute]
    public string enterBuildModeText;
    [TextAreaAttribute]
    public string placeStructureText;
    [TextAreaAttribute]
    public string changeStructureText;
    [TextAreaAttribute]
    public string BuildHighText;

    private Text textBox;

    void Start()
    {
        textBox = GetComponent<Text>();

        if(PlayerPrefs.GetInt("FinishedStructureTutorial") == 0){
            StartCoroutine(TutorialSequence());
        }else{
            textBox.text = "";
            Destroy(this);
        }
    }

    IEnumerator TutorialSequence(){
        textBox.text = "";
        yield return new WaitForSeconds(5f);

        textBox.text = enterBuildModeText;
        yield return WaitForEnterBuildMode();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = placeStructureText;
        yield return WaitForFirstStructure();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = changeStructureText;
        yield return WaitForChangeStructure();
        yield return new WaitForSeconds(timeBeforeChangingText);

        textBox.text = BuildHighText;
        yield return new WaitForSeconds(15f);

        PlayerPrefs.SetInt("FinishedStructureTutorial", 1);
        textBox.text = "";
        Destroy(this);
    }

    IEnumerator WaitForEnterBuildMode(){
        while(!Input.GetButtonDown("Build")){
            yield return null;
        }
        yield return null;
    }
    
    IEnumerator WaitForFirstStructure(){
        bool structureIsBuilt = false;
        while(!structureIsBuilt){
            foreach(Structure structure in FindObjectsOfType<Structure>()){
                structureIsBuilt |= structure.built;
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator WaitForChangeStructure(){
        while(!(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2))){
            yield return null;
        }
        yield return null;
    }
}
