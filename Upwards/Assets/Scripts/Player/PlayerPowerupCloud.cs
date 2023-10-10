using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerupCloud : MonoBehaviour
{

    public float glideSpeed;


    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private Animator anim;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetButton("Cloud") && !playerMovement.grounded && rb.velocity.y < 0f){
            playerMovement.gliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -glideSpeed, Mathf.Infinity));
        }else{
            playerMovement.gliding = false;
        }
    }
}
