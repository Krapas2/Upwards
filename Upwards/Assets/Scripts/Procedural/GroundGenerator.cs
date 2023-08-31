using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundGenerator : MonoBehaviour
{

    public Vector2 offset;
    public int width = 256;
    public int height = 256;

    public TileBase tileGround;

    public float surfaceOctave = 4;
    public float surfaceMinHeight = 300;
    public float surfaceMaxHeight = 225;
    public float surfaceCrust = 25;
    public float surfaceZoom = 4;
    
    public int caveOctave = 2;
    public float caveZoom = 2;
    public float caveTunnelWidth = -.5f;

    void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        float surfaceSeed = Random.Range(0,10000); // gets added to the position of the noise find a random part

        for(int x = 0; x < width; x++){
            for(int y = 0; y > -height; y--){
                Vector2 pos = new Vector2(x,y);

                tilemap.SetTile(new Vector3Int((x - width / 2) + (int)offset.x, y + (int)offset.y), NoiseTile(new Vector2(x+surfaceSeed,y)));
            }
        }
    }

    public TileBase NoiseTile(Vector2 pos){
        float v = NoiseWithOctaves(pos / (new Vector2(1.25f,1)), caveZoom, caveOctave);
        v = ShapeNoiseForTunnels(v);

        return v > .5 ? tileGround : null; //if value over threshold place ground
    }

    public float GetOctaveMax(int n) { //currently returning max of n-1, probably use for loop instead of recursion to fix
        if(n>0){
            return GetOctaveMax(n-1) + 1/Mathf.Pow(2,n);
        } else{
            return 1;
        }
    }

    float NoiseWithOctaves(Vector2 pos, float scale, int oct)
    {
        float v = Mathf.PerlinNoise(pos.x / scale, pos.y / scale);
        for(int i = 2; i <= oct; i++){
            float octStrength = 1/Mathf.Pow(2,i-1);
            v += Mathf.PerlinNoise((pos.x / octStrength) / scale, (pos.y / octStrength) / scale) * octStrength;
        }
        v /= GetOctaveMax(oct-1);
        return v;
    }

    float ShapeNoiseForTunnels(float v) //shape noise to create continuous tunnels
    {
        return (1-Mathf.Sin(v*Mathf.PI)) - caveTunnelWidth; //high result for low and high values, low for median
    }

    public float Map(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax){
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
        
        return NewValue;
    }
}
