using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTest : MonoBehaviour
{

    public int width = 256;
    public int height = 256;

    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.material.SetTexture("iunno", GenerateTexture());
    }

    Texture2D GenerateTexture(){
        Texture2D texture = new Texture2D(width, height);

        for(int x = 0; x < width; x++){
            for(int y = 0; y < width; y++){
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y){
        float xCoord = (float)x/width;
        float yCoord = (float)y/height;

        float sample = Mathf.PerlinNoise(xCoord,yCoord);
        return new Color(sample, sample, sample);
    }
}
