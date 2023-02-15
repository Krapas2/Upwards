using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapTest : MonoBehaviour
{

    public int width = 512;
    public int height = 256;

    public float scale = 0.0325f;

    public int minHeight = 30;

    public TileBase tileCloud;
    public TileBase tileInCloud;

    void Awake()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        float seed = Random.Range(0,10000);

        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                tilemap.SetTile(new Vector3Int(x - width / 2, y + minHeight, 0), NoiseTile(x, y, seed, (float)y/(height + 30)));
            }
        }
    }

    TileBase NoiseTile(int x, int y, float seed, float chance)
    {
        TileBase thisTile = null;
        
        float xCoord = (float)x/width/scale;
        float yCoord = (float)y/height/scale;
        
        float noise = Mathf.PerlinNoise(xCoord + seed, yCoord + seed);
        if(noise <= chance/8){
            thisTile = tileInCloud;
        } else if(noise <= chance){
            thisTile = tileCloud;
        }

        return thisTile;
    }
}
