using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildsGeneration : MonoBehaviour
{
    [HideInInspector]
    public GroundGenerator groundGenerator;
    [HideInInspector]
    public ResourceGenerator[] resourceGenerators;

    void Start()
    {
        groundGenerator = FindObjectOfType<GroundGenerator>();
        resourceGenerators = FindObjectsOfType<ResourceGenerator>();

        StartCoroutine(Seq());
    }

    private IEnumerator Seq()
    {
        yield return StartCoroutine(GenerateGround());
        yield return StartCoroutine(GenerateResources());
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
}
