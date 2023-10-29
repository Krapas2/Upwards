using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float index;

    private Transform cam;
    
    private Vector3 origPos;
    private Vector3 origCamPos;

    void Start()
    {
        cam = Camera.main.transform;

        origPos = transform.position;
        origCamPos = cam.position;
    }

    void LateUpdate()
    {
        transform.position = origPos + Vector3.Lerp(origCamPos,cam.position,index);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
