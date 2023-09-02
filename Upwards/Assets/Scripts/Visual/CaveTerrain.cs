using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CaveTerrain : MonoBehaviour
{
    public Transform lightSource;

    private TilemapRenderer sprite;

    void Start()
    {
        sprite = GetComponent<TilemapRenderer>();
    }

    void Update()
    {
        sprite.material.SetVector("_LightPos",lightSource.position);
    }
}
