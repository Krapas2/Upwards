using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCloudManager : MonoBehaviour
{
    
    public GameObject EndCloud;
    public Vector2 minPos;
    public Vector2 maxPos;

    public GameObject ObjectivePointer;

    public float PointerDistance;
    
    [HideInInspector]
    public int particleAmount = 1;
    
    private PlayerPowerupManager playerPowerups;

    void Start()
    {
        playerPowerups = FindObjectOfType<PlayerPowerupManager>();

        EndCloud = Instantiate(EndCloud, new Vector2(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y)), Quaternion.identity);
        ObjectivePointer = Instantiate(ObjectivePointer, playerPowerups.transform.position, Quaternion.identity);

        ObjectivePointer.SetActive(playerPowerups.HasAllPowerups());
    }

    void Update()
    {
        if(EndCloud){
            ObjectivePointer.transform.up = EndCloud.transform.position - ObjectivePointer.transform.position;
            if(Vector3.Distance(playerPowerups.transform.position, EndCloud.transform.position) > PointerDistance){
                ObjectivePointer.transform.position = (EndCloud.transform.position - playerPowerups.transform.position).normalized * PointerDistance + playerPowerups.transform.position;
            } else {
                ObjectivePointer.transform.position = EndCloud.transform.position + Vector3.up * 2;
            }
        }
        else{
            if(ObjectivePointer)
                Destroy(ObjectivePointer.gameObject);
            
            if(particleAmount == 0){
                SceneManager.LoadScene("Outro");
            }
        }
    }
}
