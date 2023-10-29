using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildsGeneration : MonoBehaviour
{
    [HideInInspector]
    public GroundGenerator groundGenerator;
    [HideInInspector]
    public ResourceGenerator[] resourceGenerators;
    [HideInInspector]
    public SceneSpawn sceneSpawn;

    void Awake()
    {
        groundGenerator = FindObjectOfType<GroundGenerator>();
        resourceGenerators = FindObjectsOfType<ResourceGenerator>();
        sceneSpawn = FindObjectOfType<SceneSpawn>();

        StartCoroutine(Seq());
    }

    private IEnumerator Seq()
    {
        yield return StartCoroutine(GenerateGround());
        yield return StartCoroutine(GenerateResources());
        yield return StartCoroutine(SetPlayerOnSpawn());
    }

    IEnumerator GenerateGround(){ 
        if(groundGenerator)
            groundGenerator.Generate();
        yield return null;
    }
    IEnumerator GenerateResources(){ 
        if(resourceGenerators.Length > 0){
            foreach(ResourceGenerator resourceGenerator in resourceGenerators){
                resourceGenerator.Generate();
            }
        }
        yield return null;
    }
    IEnumerator SetPlayerOnSpawn(){
        if(sceneSpawn){
            sceneSpawn.SetPlayerOnSpawn();
        }
        yield return null;
    }
}
