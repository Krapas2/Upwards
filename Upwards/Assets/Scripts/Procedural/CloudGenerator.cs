using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CloudGenerator : MonoBehaviour
{

    public Vector2 offset;
    public int width = 256;
    public int height = 256;

    public TileBase tileCloud;

    public int gradientOctave = 2;
    public float gradientZoom = 40;
    public float gradientWeight = .7f;  // used for weighted average of noises
    public float worleyDensity;

    private float[] octaveSeeds = new float[10];

    private WorleyNoise worleyNoise;

    void Start()
    {
        worleyNoise = GetComponent<WorleyNoise>();

        Generate();
    }

    public void Generate()
    {
        Tilemap tilemap = GetComponent<Tilemap>();

        for (int i = 0; i < octaveSeeds.Length; i++)
        {
            octaveSeeds[i] = Random.Range(0, 16384); // gets added to the position of the noise find a random part of it
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = new Vector2(x, y);
                TileBase tile = NoiseTile(pos);
                tilemap.SetTile(new Vector3Int(x - width / 2 + (int)offset.x, y + (int)offset.y), tile);
            }
        }
    }

    public TileBase NoiseTile(Vector2 pos)
    {
        float perlin = NoiseWithOctaves(pos / new Vector2(1.25f, 1), gradientZoom, gradientOctave);
        float worley = 1 - worleyNoise.GetNoise(pos, new Vector2(width, height), worleyDensity);

        float v = (perlin * gradientWeight) + (worley * (1 - gradientWeight));
        v += Map(pos.y, 0f, height, -.5f, .5f) * .4f;

        return (v > .5) ? tileCloud : null; //if value over threshold place ground
    }

    public float GetOctaveMax(int n)
    { //currently returning max of n-1, probably use for loop instead of recursion to fix
        if (n > 0)
        {
            return GetOctaveMax(n - 1) + 1 / Mathf.Pow(2, n);
        }
        else
        {
            return 1;
        }
    }

    float NoiseWithOctaves(Vector2 pos, float scale, int oct)
    {
        float v = Mathf.PerlinNoise((pos.x / scale) + octaveSeeds[1], pos.y / scale);

        for (int i = 2; i <= oct; i++)
        {
            float octStrength = 1 / Mathf.Pow(2, i - 1);
            v += Mathf.PerlinNoise((pos.x / octStrength / scale) + octaveSeeds[i], pos.y / octStrength / scale) * octStrength;
        }
        v /= GetOctaveMax(oct - 1);
        return v;
    }

    public float Map(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax)
    {
        float OldRange = OldMax - OldMin;
        float NewRange = NewMax - NewMin;
        float NewValue = ((OldValue - OldMin) * NewRange / OldRange) + NewMin;

        return NewValue;
    }
}
