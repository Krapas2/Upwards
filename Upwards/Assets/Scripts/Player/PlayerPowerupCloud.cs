using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerupCloud : MonoBehaviour
{

    public Vector2 glideSpeed;
    public float glideTime;

    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    private AudioManager _audManager;
    private bool riseBool;
    private bool fadeBool;
    [HideInInspector]
    public float glideTimer;

    void Start()
    {
        _audManager = FindObjectOfType<AudioManager>();

        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        riseBool = true;
        fadeBool = true;
        glideTimer = 0;
    }

    void LateUpdate()
    {
        if(playerMovement.grounded){
            glideTimer = 0;
        }

        if (Input.GetButton("Cloud") && glideTimer < glideTime)
        {
            if(riseBool) {
                if(_audManager) _audManager.Play("CloudRise");
                riseBool= false;
            }

            fadeBool = true;
            playerMovement.gliding = true;
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -glideSpeed.x, glideSpeed.x), Mathf.Max(rb.velocity.y, glideSpeed.y));
            glideTimer += Time.deltaTime;
        }
        

        if (Input.GetButtonUp("Cloud") || !(glideTimer < glideTime))
        {
            if(fadeBool)
            {
                FadeOut();
            }
            playerMovement.gliding = false;
            CancelInvoke();
        }
    }

    void FadeOut()
    {
        if (_audManager)
        {
            _audManager.Play("CloudPop");
            _audManager.Stop("CloudRise");
        }
        fadeBool = false;
        riseBool = true;
    }
}