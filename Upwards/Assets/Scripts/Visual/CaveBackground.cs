using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBackground : MonoBehaviour
{

    public Transform lightSource;

    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        sprite.material.SetVector("_LightPos",lightSource.position);
    }
}
