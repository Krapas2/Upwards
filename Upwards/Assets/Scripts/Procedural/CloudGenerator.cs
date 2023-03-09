using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CloudGenerator : MonoBehaviour
{

    public int width = 256;
    public int height = 512;

    public float xScale = 0.0325f;
    public float yScale = 0.0325f;

    public int minHeight = 0;

    public TileBase tileCloud;

    void Awake()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        float seed = Random.Range(0,10000); // gets added onto the position of the noise find a random part of the noise

        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                float chance = ThresholdFromPosition((float)x, (float)y);
                tilemap.SetTile(new Vector3Int(x - width / 2, y + minHeight, 0), NoiseTile(x, y, seed, chance));
            }
        }
    }

    TileBase NoiseTile(int x, int y, float seed, float chance)
    {
        TileBase thisTile = null;
        
        float xCoord = (float)x/width/xScale;
        float yCoord = (float)y/height/yScale;
        
        float noise = Mathf.PerlinNoise(xCoord + seed, yCoord + seed);
        if(noise <= chance){
            thisTile = tileCloud;
        }

        return thisTile;
    }

    float ThresholdFromPosition(float x, float y){ //x is currently irrelevant
        return (Mathf.Sin(y/(Mathf.PI*4))+1)/2;
    }
}
