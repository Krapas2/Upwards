using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemSourceLight : MonoBehaviour
{
    public Transform lightSource;

    private Renderer sprite;

    void Start()
    {
        sprite = GetComponent<Renderer>();

        if(!lightSource){
            lightSource = FindObjectOfType<PlayerMovement>().transform;
        }
    }

    void Update()
    {
        sprite.material.SetVector("_LightPos",lightSource.position);
    }
}
