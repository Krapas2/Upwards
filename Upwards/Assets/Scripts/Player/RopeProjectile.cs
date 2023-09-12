using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RopeProjectile : MonoBehaviour
{

    public LayerMask ground;

    [HideInInspector]
    public bool travelling = true;

    [HideInInspector]
    public Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(travelling){
            transform.up = rb.velocity;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(ground == (ground | (1 << col.gameObject.layer))){
            travelling = false;
            rb.bodyType = RigidbodyType2D.Static;
            Destroy(GetComponent<Collider2D>());
        }
    }
}
