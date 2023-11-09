using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerupBroom : MonoBehaviour
{
    public float flightSpeed;
    public float flightTime;

    private float origSpeed;
    private float origGravity;

    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    private AudioManager _audManager;
    private bool fadeBool;
    [HideInInspector]
    public float flightTimer;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        _audManager = FindObjectOfType<AudioManager>();

        origSpeed = playerMovement.walkSpeed;
        origGravity = rb.gravityScale;

        fadeBool = true;
        flightTimer = 0;
    }

    void LateUpdate()
    {
        if (playerMovement.grounded)
        {
            flightTimer = 0;
        }

        bool buttonComboDown = (
            (Input.GetButton("Jump") && Input.GetButtonDown("Broom")) ||
            (Input.GetButtonDown("Jump") && Input.GetButton("Broom")) ||
            (Input.GetButtonDown("Jump") && Input.GetButtonDown("Broom"))
        );

        bool buttonComboUp = (
            (Input.GetButton("Jump") && Input.GetButtonUp("Broom")) ||
            (Input.GetButtonUp("Jump") && Input.GetButton("Broom")) ||
            (Input.GetButtonUp("Jump") && Input.GetButtonUp("Broom"))
        );

        if (buttonComboDown && flightTimer < flightTime && !playerMovement.gliding)
        {
            fadeBool = true;
            if (_audManager)
            {
                _audManager.Play("Broom");
                _audManager.Play("Wind");
            }

            rb.velocity = new Vector2(rb.velocity.x, 0);
            playerMovement.flying = true;
            playerMovement.walkSpeed = flightSpeed;
            rb.gravityScale = 0f;
        }

        if (playerMovement.flying)
        {
            flightTimer += Time.deltaTime;
        }

        if (buttonComboUp || !(flightTimer < flightTime))
        {
            if (fadeBool)
            {
                FadeOut();
            }
            ResetPhysics();
        }

    }

    void ResetPhysics()
    {
        playerMovement.flying = false;
        playerMovement.walkSpeed = origSpeed;
        rb.gravityScale = origGravity;
        CancelInvoke();
    }

    void FadeOut()
    {
        if (_audManager)
        {
            _audManager.Play("WindFadeOut");
            _audManager.Stop("Wind");
        }
        fadeBool = false;
    }
}
