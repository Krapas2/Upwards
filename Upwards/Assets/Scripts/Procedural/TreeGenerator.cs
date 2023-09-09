using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public GameObject tree;
    public float minX; //lowest x coordinate to place trees
    public float maxX; //highest x coordinate to place trees
    public float maxY; //used cast a ray to find coordinate of ground
    public LayerMask ground;
    public int amount;

    private int curAmount = 0;
    void Start()
    {
        //Generate();
    }

    public void Generate(){
        float seed = Random.Range(0,10000);
        while(curAmount < amount){
            float randomPos = Random.Range(minX,maxX);
            float densityInPosition = Mathf.PerlinNoise(randomPos + seed, seed);
            if(Random.Range(0f,1f) > densityInPosition){ // higher chance of being placed if noise gives higher density, clumps resources in groups
                Vector2 highPos = new Vector2(randomPos, maxY);
                Vector2 pos = Physics2D.Raycast(highPos, Vector2.down, Mathf.Infinity, ground).point; // real position should be placed on ground
                pos += Vector2.down/16; // bury object one pixel in ground for visuals
                Instantiate(tree, pos, Quaternion.identity, transform);
                curAmount++;
            }
        }
    }
}
