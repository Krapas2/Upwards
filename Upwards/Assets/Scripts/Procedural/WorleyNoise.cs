using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WorleyNoise : MonoBehaviour
{

    public float GetNoise(Vector2 pos, Vector2 resolution, float density) { //ported from thebookofshaders.com/12
        Vector2 normalizedPos = pos / resolution;
        normalizedPos.x *= resolution.x/resolution.y;

        // Scale
        normalizedPos *= density;

        // Tile the space
        Vector2 posOfCoreTile = new Vector2(Mathf.Floor(normalizedPos.x), Mathf.Floor(normalizedPos.y));
        Vector2 posInTile = new Vector2(normalizedPos.x % 1, normalizedPos.y % 1);


        float mimimumDistance = 1;
        for (int y= -1; y <= 1; y++) {
            for (int x= -1; x <= 1; x++) {
                // Neighbor place in the grid
                Vector2 posOfCurNeighbor = new Vector2((float)x,(float)y);

                // Random position from current + neighbor place in the grid
                Vector2 point = random2(posOfCoreTile + posOfCurNeighbor);

                // Vector between the pixel and the point
                Vector2 diff = posOfCurNeighbor + point - posInTile;

                // Distance to the point
                float curDistance = Vector2.Distance(Vector2.zero, diff);

                // Keep the closer distance
                mimimumDistance = Mathf.Min(mimimumDistance, curDistance);
            }
        }

        // Draw the min distance (distance field)
        return mimimumDistance;
    }

    private Vector2 random2(Vector2 pos) { //pseudo rng
        Vector2 v = new Vector2(
                        Vector3.Dot(
                            pos,
                            new Vector2(127.1f, 311.7f)
                        ), 
                        Vector3.Dot(
                            pos,
                            new Vector2(269.5f, 183.3f)
                        )
                    );
        v = new Vector2(Mathf.Sin(v.x), Mathf.Sin(v.y)) * 43758.5453f;
        v = new Vector2(v.x % 1, v.y % 1);
        v = new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
        return v;
    }
}