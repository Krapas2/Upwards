using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRope : MonoBehaviour
{

    public RopeProjectile rope;
    public float throwSpeed;
    public float distanceOffsetFactor;



    private Camera cam;

    void Start(){
        cam = Camera.main;

    }

    void Update()
    {
        if(Input.GetButtonDown("Fire2")){
            ThrowRope();
        } 
    }

    void ThrowRope(){
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float yOffset = Mathf.Pow(Vector2.Distance(transform.position, mousePos),2) / distanceOffsetFactor;

        RopeProjectile curRope = Instantiate(rope, transform.position, Quaternion.identity);
        curRope.rb.velocity = ((mousePos - (Vector2)transform.position) + new Vector2(0, yOffset)).normalized * throwSpeed;
    }
}
