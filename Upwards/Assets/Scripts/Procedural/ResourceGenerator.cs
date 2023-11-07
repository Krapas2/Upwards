using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{

    public ItemSourceType resourceType;
    public float minX; //lowest x coordinate to place trees
    public float maxX; //highest x coordinate to place trees
    public float minY; //lowest y coordinate to place trees
    public float maxY; //highest y coordinate to place trees
    public float noiseThreshold;
    public LayerMask ground;
    public int amount;

    public void Generate()
    {
        float seed = Random.Range(0,10000);
        int curAmount = 0;
        while(curAmount < amount){
            Vector2 randomPos = new Vector2(Random.Range(minX,maxX), Random.Range(minY,maxY)); //raffle position to try to place
            float densityInPosition = Mathf.PerlinNoise(randomPos.x + seed, randomPos.y);
            bool posInGeometry = Physics2D.OverlapPoint(randomPos, ground);
            if(densityInPosition > noiseThreshold && !posInGeometry){
                Vector2 highPos = randomPos;
                Vector2 pos = Physics2D.Raycast(highPos, Vector2.down, Mathf.Infinity, ground).point;
                pos += Vector2.down / 16f;
                int itemSourceIndex = (int)(Mathf.Pow(Random.Range(0f, 1f),2)*resourceType.itemSources.Length);
                Instantiate(resourceType.itemSources[itemSourceIndex], pos, Quaternion.identity, transform);
                curAmount++;
            }
        }
    }

    public float Map(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax){
        float OldRange = OldMax - OldMin;
        float NewRange = NewMax - NewMin;
        float NewValue = ((OldValue - OldMin) * NewRange / OldRange) + NewMin;
        
        return NewValue;
    }
}
