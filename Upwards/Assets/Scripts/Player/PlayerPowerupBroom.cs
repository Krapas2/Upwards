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

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        canFly = true;
    }

    void Update()
    {
        canFly |= playerMovement.grounded;

        if (Input.GetButtonDown("Broom") && canFly)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            playerMovement.walkSpeed = flightSpeed;
            playerMovement.flying = true;
            rb.gravityScale = 0f;
            Invoke("EndFlight", flightTime);
        }
        if (Input.GetButtonUp("Broom") || !canFly)
        {
            CancelInvoke();
            playerMovement.flying = false;
        }
    }

    void EndFlight()
    {
        canFly = false;
    }
}
