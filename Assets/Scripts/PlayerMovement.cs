using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Private
    Rigidbody2D rigidbody2D;
    bool isGrounded = false;
    bool isHoldingSpace;
    float jumpTimeCounter;
    float lastTimeGrounded;
    private PlayerAnimation pAnimation;
    private Animator animator;
    private float currentSpeed;
    //Public
    [Header("Player Movement")]
    [Tooltip("Speed at which the player moves.")]
    public float movementSpeed = 3f;
    public float maxSpeed = 9f;
    public float incrementAcceleration = 0.01f;
    [Header("Player Jump")]
    [Tooltip("Force of jump.")]
    public float jumpForce = 4f;
    [Tooltip("How fast the player falling is accelerated as time goes by.")]
    public float fallMultiplier = 0.2f;
    [Tooltip("How long after player can still jump after leaving ground in seconds.")]
    public float groundedRememberTime = 0.2f;
    [Tooltip("How long the player can hold spacebar to jump higher.")]
    public float jumpSpacebarMaxTime = 0.35f;

    [Header("Others")]
    public Transform isGroundedTransform;
    public LayerMask GroundLayer;
    
    

    

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        pAnimation = GetComponent<PlayerAnimation>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckIfGrounded();
        MovePlayer();
        JumpPlayer();
        ImproveJump();
        
    }

    private void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedTransform.position, 0.2f, GroundLayer);

        if(colliders != null)
        {
            isGrounded = true;

        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
                
            }
            isGrounded = false;
        }
    }

    void MovePlayer()
    {
        float xAxisInput = Input.GetAxisRaw("Horizontal");
        float moveAmount = xAxisInput * maxSpeed;

        if (moveAmount > 0)
        {
            pAnimation.Moving("Right");
            currentSpeed += incrementAcceleration;
            moveAmount= Mathf.Clamp(moveAmount, 0, currentSpeed);
        }
            
        else if (moveAmount < 0)
        {
            pAnimation.Moving("Left");
            currentSpeed += incrementAcceleration;
            moveAmount = Mathf.Clamp(moveAmount, -currentSpeed, 0);
        }

        else
        {
            pAnimation.Idle();
            currentSpeed = movementSpeed;
        }

       
        animator.speed = Mathf.Abs(moveAmount/maxSpeed);

        rigidbody2D.velocity = new Vector2(moveAmount, rigidbody2D.velocity.y);
    }

    void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded  || Time.time - lastTimeGrounded <= groundedRememberTime))
        {
            isHoldingSpace = true;
            jumpTimeCounter = jumpSpacebarMaxTime;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            
        }

        if (Input.GetKey(KeyCode.Space) && isHoldingSpace == true )
        {

            if (jumpTimeCounter > 0)
            {
                
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                
                isHoldingSpace = false;

            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            
            isHoldingSpace = false;
        }
    }

    void ImproveJump()
    {
        if (Math.Round(rigidbody2D.velocity.y, 2) < 0)
        {
            rigidbody2D.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
            pAnimation.Falling();
        }
        else if(Math.Round(rigidbody2D.velocity.y, 2) > 0 )
        {
            pAnimation.Jumping();
        }
        else
        {
            pAnimation.NotFalling();
        }
    }

}
