using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerupBroom : MonoBehaviour
{
    public float flightSpeed;
    public float flightTime;

    private bool canFly;
    private float origSpeed;
    private float origGravity;

    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        canFly = true;

        origSpeed = playerMovement.walkSpeed;
        origGravity = rb.gravityScale;
    }

    void Update()
    {
        canFly |= playerMovement.grounded;

        if(Input.GetButtonDown("Broom") && canFly){
            rb.velocity = new Vector2(rb.velocity.x, 0);
            playerMovement.walkSpeed = flightSpeed;
            playerMovement.flying = true;
            rb.gravityScale = 0f;
            Invoke("EndFlight", flightTime);
        }
        if(Input.GetButtonUp("Broom") || !canFly){
            playerMovement.flying = false;
            playerMovement.walkSpeed = origSpeed;
            rb.gravityScale = origGravity;
        }
    }

    void EndFlight(){
        canFly = false;
    }
}
