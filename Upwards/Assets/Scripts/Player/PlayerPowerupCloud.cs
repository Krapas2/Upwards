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

    private AudioManager _audManager;
    private bool riseBool;
    private bool fadeBool;

    void Start()
    {
        _audManager = FindObjectOfType<AudioManager>();

        canGlide = true; riseBool = true; fadeBool = true;
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        riseBool = true; fadeBool = true;
        
    }

    void LateUpdate()
    {
        canGlide |= playerMovement.grounded;

        if (Input.GetButton("Cloud") && canGlide)
        {
            if(riseBool) {
                if(_audManager) _audManager.Play("CloudRise");
                riseBool= false;
            }

            fadeBool = true;
            playerMovement.gliding = true;
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -glideSpeed.x, glideSpeed.x), Mathf.Max(rb.velocity.y, glideSpeed.y));
            Invoke("EndGlide", glideTime);
        }
        if (Input.GetButtonUp("Cloud") || !canGlide)
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


    void EndGlide()
    {
        canGlide = false;
    }
}
