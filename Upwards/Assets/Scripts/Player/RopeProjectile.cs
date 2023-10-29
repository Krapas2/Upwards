using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RopeProjectile : MonoBehaviour
{

    public RopeExtension rope;
    public Transform ropeSpawnPoint;
    public LayerMask ground;

    [HideInInspector]
    public bool travelling = true;

    [HideInInspector]
    public Rigidbody2D rb;
    private AudioManager _audManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _audManager = FindObjectOfType<AudioManager>();
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
            _audManager.Play("RopeHit");
            Destroy(GetComponent<Collider2D>());
            SpawnRope();
        }
    }

    void SpawnRope(){
        Instantiate(rope, ropeSpawnPoint.position + Vector3.down / 2f, Quaternion.identity, transform);
    }
}
