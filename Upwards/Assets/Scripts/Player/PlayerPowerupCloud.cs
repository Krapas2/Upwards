using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerupCloud : MonoBehaviour
{

    public Vector2 glideSpeed;
    public float glideTime;

    private bool canGlide;

    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    void Start()
    {
        canGlide = true;
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        canGlide |= playerMovement.grounded;

        if (Input.GetButton("Cloud") && canGlide)
        {
            playerMovement.gliding = true;
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -glideSpeed.x, glideSpeed.x), Mathf.Max(rb.velocity.y, glideSpeed.y));
            Invoke("EndGlide", glideTime);
        }
        if (Input.GetButtonUp("Cloud") || !canGlide)
        {
            playerMovement.gliding = false;
            CancelInvoke();
        }
    }

    void EndGlide()
    {
        canGlide = false;
    }
}
