using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterReflectionBehaviour : MonoBehaviour
{
    public float SquishFactor;
    public float SquishMultiplier;

    private Renderer sprite;
    private Camera cam;

    void Start()
    {
        sprite = GetComponent<Renderer>();

        cam = Camera.main;
    }

    void Update()
    {
        sprite.material.SetVector("_SquishFactor",new Vector3(1, SquishFactor - (cam.transform.position.y*SquishMultiplier), 1));
        transform.position = new Vector3(cam.transform.position.x, transform.position.y, transform.position.z);
    }
}
