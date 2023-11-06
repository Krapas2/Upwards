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

    public int surfaceNoiseOctave = 4;
    public float surfaceNoiseScale = 15;
    public int surfaceDepth = 50;
    public int surfaceCrust = 100;
    
    public int caveNoiseOctave = 2;
    public float caveNoiseZoom = 40;
    public float caveTunnelWidth = -.49f;

    public float borderThickness = 15;

    public Tilemap background;
    public TileBase tileBackground;

    private float[] octaveSeeds = new float[10];

    void Start()
    {
        //Generate();
    }

    public void Generate(){
        Tilemap tilemap = GetComponent<Tilemap>();

        for(int i = 0; i < octaveSeeds.Length; i++){
            octaveSeeds[i] = Random.Range(0,16384); // gets added to the position of the noise find a random part of it
        }

        for(int x = 0; x < width; x++){
            int surfaceHeight = (int)Map(NoiseWithOctaves(new Vector2(x,0), surfaceNoiseScale, surfaceNoiseOctave),0,1,height-surfaceDepth,height);

            for(int y = 0; y < height; y++){
                if(x == 0 && y == 0){
                }
                Vector2 pos = new Vector2(x,y);
                TileBase tile = NoiseTile(pos, surfaceHeight);
                tilemap.SetTile(new Vector3Int(x - width / 2 + (int)offset.x, y + (int)offset.y), tile);
                background.SetTile(new Vector3Int(x - width / 2 + (int)offset.x, y + (int)offset.y), y < (surfaceHeight) ? tileBackground : null);
            }
        }
    }

    public TileBase NoiseTile(Vector2 pos, int surfaceHeight){
        float v = NoiseWithOctaves(pos / new Vector2(1.25f,1), caveNoiseZoom, caveNoiseOctave);
        v = ShapeNoiseForTunnels(v);
        v += pos.y > surfaceHeight-surfaceCrust ? Map(pos.y,surfaceHeight-surfaceCrust,surfaceHeight,0,.0075f) : 0; //raise threshold if close to surface
        v += pos.y < borderThickness ? Map(pos.y,0,borderThickness,.025f,0) : 0; // bottom border
        v += pos.x < borderThickness ? Map(pos.x,0,borderThickness,.025f,0) : 0; // left border
        v += pos.x > width-borderThickness ? Map(pos.x,width-borderThickness,width,0,.025f) : 0; // right border

        return (v > .5) && (pos.y < surfaceHeight) ? tileGround : null; //if value over threshold place ground
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
        float v = Mathf.PerlinNoise((pos.x / scale) + octaveSeeds[1], pos.y / scale);

        for(int i = 2; i <= oct; i++){
            float octStrength = 1/Mathf.Pow(2,i-1);
            v += Mathf.PerlinNoise((pos.x / octStrength / scale) + octaveSeeds[i], pos.y / octStrength / scale) * octStrength;
        }
        v /= GetOctaveMax(oct-1);
        return v;
    }

    float ShapeNoiseForTunnels(float v) //shape noise to create continuous tunnels
    {
        return 1 - Mathf.Sin(v*Mathf.PI) - caveTunnelWidth; //high result for low and high values, low for median
    }

    public float Map(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax){
        float OldRange = OldMax - OldMin;
        float NewRange = NewMax - NewMin;
        float NewValue = ((OldValue - OldMin) * NewRange / OldRange) + NewMin;
        
        return NewValue;
    }
}
