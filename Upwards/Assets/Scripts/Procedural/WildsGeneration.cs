using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildsGeneration : MonoBehaviour
{

    [HideInInspector]
    public GroundGenerator groundGenerator;
    [HideInInspector]
    public TreeGenerator treeGenerator;
    [HideInInspector]
    public RockGenerator rockGenerator;

    void Start()
    {
        groundGenerator = FindObjectOfType<GroundGenerator>();
        treeGenerator = FindObjectOfType<TreeGenerator>();
        rockGenerator = FindObjectOfType<RockGenerator>();
/*
        groundGenerator.Generate();
        treeGenerator.Generate();*/
        //rockGenerator.Generate();
        StartCoroutine(Seq());
    }

    private IEnumerator Seq()
    {
        yield return StartCoroutine(GenerateGround());
        yield return StartCoroutine(GenerateTrees());
        yield return StartCoroutine(GenerateRocks());
    }

    IEnumerator GenerateGround(){ 
        groundGenerator.Generate();
        yield return null;
    }
    IEnumerator GenerateTrees(){ 
        treeGenerator.Generate();
        yield return null;
    }
    IEnumerator GenerateRocks(){ 
        rockGenerator.Generate();
        yield return null;
    }
}
