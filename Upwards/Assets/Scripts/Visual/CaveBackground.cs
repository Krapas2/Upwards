using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBackground : MonoBehaviour
{

    public Transform lightSource;

    private Renderer sprite;

    void Start()
    {
        sprite = GetComponent<Renderer>();
    }

    void Update()
    {
        sprite.material.SetVector("_LightPos",lightSource.position);
    }
}
