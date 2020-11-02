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

    //Public
    [Header("Player Movement")]
    [Tooltip("Speed at which the player moves.")]
    public float movementSpeed = 3f;

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
        float moveAmount = xAxisInput * movementSpeed;

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
        if (rigidbody2D.velocity.y < 0)
        {
            rigidbody2D.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

}
