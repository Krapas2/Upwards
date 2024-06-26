﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //-------------------walking-------------------
    public float walkSpeed = 15;
    public float slowInAir = 7f; // amount to slow down when in air and not pressing movement keys
    //private bool walkSFXBool;

    //-------------------jumping-------------------
    public float jumpForce = 30;
    public float fallMultiplier = 2.5f; // value to increase speed when falling
	public float lowJumpMultiplier = 2f; // value to end jump short when jump key released
    public Transform groundCheck;
    public LayerMask ground;

    //-------------------rope climbing--------------------
    public float ropeClimbSpeed;
    public float ropeHopForce;
    public Transform ropeCheck;
    public LayerMask ropeLayer;


    [HideInInspector]
    public Collider2D curRope;

    [HideInInspector]
    public Vector3 climbPosition;

    [HideInInspector]
    public bool climbing;

    private bool climbSFXBool;

    //-------------------Misc-------------------

    public LayerMask platformLayer; // layer with objects to fall through when holding down

    private float horizontalInput;
    private float verticalInput;

    [HideInInspector]
    public bool grounded;
    [HideInInspector]
    public bool flying;
    
    [HideInInspector]
    public bool gliding;
    private bool facingRight = true;

    //-------------------Components-------------------
    private Rigidbody2D rb;
    private Animator anim;
    private AudioManager _audManager;

    void Start()
    {
        //-------------------assigning components-------------------
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _audManager = FindObjectOfType<AudioManager>();

        //-------------------assigning vars-------------------
        climbSFXBool = true;
        //walkSFXBool= true;

    }


    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        grounded = Physics2D.OverlapCircle(groundCheck.position, .2f, ground);
        curRope = Physics2D.OverlapPoint(ropeCheck.position,ropeLayer);
        
        //-------------------walking-------------------
        if(!climbing){
            if (grounded || horizontalInput != 0)
            {
                rb.velocity = new Vector2(horizontalInput * walkSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(horizontalInput * walkSpeed, rb.velocity.y), slowInAir * Time.deltaTime);
                
            }

            //if (grounded && horizontalInput != 0 && walkSFXBool)
            //{
            //    if (_audManager) { _audManager.Play("footStep1"); }
            //    walkSFXBool = false;
            //}
            //else {
            //    _audManager.Stop("footStep1");
            //    walkSFXBool = true; }
        }

        if (horizontalInput > 0 && !facingRight)
            Flip ();
        else if (horizontalInput < 0 && facingRight) 
            Flip ();


        //-------------------rope climbing----------------

        if(curRope != null && !climbing && verticalInput != 0){
            transform.position = curRope.transform.position;
            rb.bodyType = RigidbodyType2D.Kinematic;
            climbing = true;
        }

        if(climbing){
            if (climbSFXBool)
            {
                if (_audManager)
                    _audManager.Play("RopeClimb");
                climbSFXBool= false;
            }
            rb.velocity = new Vector3(0f, verticalInput * ropeClimbSpeed,  0f);

            if(!curRope){
                StopClimbing(verticalInput > 0);
            }
        }

        //-------------------jump-------------------

        if (Input.GetButtonDown("Jump") && ((grounded || climbing) && !flying)) 
            Jump();
        
        if(rb.velocity.y < 0) //better jump from https://youtu.be/7KiK0Aqtmzc
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
			rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        //-------------------fall through platform-------------------
        
        Collider2D platform = Physics2D.OverlapCircle(groundCheck.position, .1f, platformLayer);
        if(platform != null && Input.GetAxisRaw("Vertical") < 0){
            StartCoroutine("FallThroughPlatform", platform.GetComponent<PlatformEffector2D>());
        }

        //-------------------ANIMAÇÃO-------------------
        if(flying)
            anim.Play("PlayerFlight");
        else if(gliding)
            anim.Play("PlayerGlide");
        else if(grounded){
            if(Mathf.Abs(rb.velocity.x) > 0)
                anim.Play("PlayerWalk");
            else
                anim.Play("PlayerIdle");
        } else{
            if (rb.velocity.y > 5f)
                anim.Play("PlayerJumpRise");
            else if (rb.velocity.y > -5f)
                anim.Play("PlayerJumpPeak");
            else
                anim.Play("PlayerJumpFall");
        }
    }

    void Jump(){
        if(climbing){
            StopClimbing(true);
        }

        if(_audManager)
            _audManager.Play("Pulo");

        rb.velocity += Vector2.up*jumpForce;
    }

    void StopClimbing(bool hop){
        rb.bodyType = RigidbodyType2D.Dynamic;
        if(hop){
            rb.velocity += Vector2.up*ropeHopForce;
        }
        if(_audManager) _audManager.Stop("RopeClimb");
        climbSFXBool = true;
        climbing = false;
    }

    public void Flip ()
	{
		facingRight = !facingRight;
		Vector3 tempScale = transform.localScale;
		tempScale.x *= -1;
		transform.localScale = tempScale;
	}

    IEnumerator FallThroughPlatform(PlatformEffector2D platform)
    {
        platform.rotationalOffset = 180f;
        yield return new WaitForSeconds (.5f);
        platform.rotationalOffset = 0f;
    }
}
