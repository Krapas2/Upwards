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
        if(!lightSource)
            lightSource = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        sprite.material.SetVector("_LightPos", lightSource.position);
    }
}
