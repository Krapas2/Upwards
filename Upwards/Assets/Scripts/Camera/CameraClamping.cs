using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamping : MonoBehaviour
{

    
	public Vector2 minCameraPos;
	public Vector2 maxCameraPos;

    private Vector2 minCameraBounds;
    private Vector2 maxCameraBounds;

    private Camera cam;
    
    void Start()
    {
        cam = GetComponent<Camera>();

        minCameraBounds = (Vector2)(cam.ScreenToWorldPoint(Vector2.zero) - transform.position);
        maxCameraBounds = (Vector2)(cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth, cam.pixelHeight)) - transform.position);
    }

    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minCameraPos.x - minCameraBounds.x, maxCameraPos.x - maxCameraBounds.x),
            Mathf.Clamp(transform.position.y, minCameraPos.y - minCameraBounds.y, maxCameraPos.y - maxCameraBounds.y),
            -10
        );

    }

    void LateUpdate()
    {
        minCameraBounds = (Vector2)(cam.ScreenToWorldPoint(Vector2.zero) - transform.position);
        maxCameraBounds = (Vector2)(cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth, cam.pixelHeight)) - transform.position);
        Debug.Log(minCameraBounds);
        Debug.Log(maxCameraBounds);
        
    }
}
