using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    public GameObject rock;
    public float minX; //lowest x coordinate to place trees
    public float maxX; //highest x coordinate to place trees
    public float minY; //used cast a ray to find coordinate of ground
    public float maxY; //used cast a ray to find coordinate of ground
    public LayerMask ground;
    public int amount;

    private int curAmount = 0;

    void Start()
    {
        
    }    
    public void Generate(){
        float seed = Random.Range(0,10000);
        while(curAmount < amount){
            Vector2 randomPos = new Vector2(Random.Range(minX,maxX), Random.Range(minY,maxY)); //raffle position to try to place
            float densityInPosition = Mathf.PerlinNoise(randomPos.x + seed, randomPos.y);
            if(Random.Range(0f,1f) > densityInPosition || !Physics2D.OverlapPoint(randomPos, ground)){
                Vector2 highPos = randomPos;
                Vector2 pos = Physics2D.Raycast(highPos, Vector2.down, Mathf.Infinity, ground).point;
                Instantiate(rock, pos, Quaternion.identity, transform);
                curAmount++;
            }
        }
    }
}
