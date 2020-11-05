using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramAnimator : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer renderer;

    Vector3 oldPosition;

  
    void Start()
    {

        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        oldPosition = new Vector3(0, 0, 0);
        oldPosition = transform.position;

    }

    void FixedUpdate()
    {
        if (oldPosition.y < transform.position.y) // Jumping
        {          
            Jumping();            
        }
        else if(oldPosition.y > transform.position.y) //Falling
        {
            Falling();
        }
        else if (oldPosition.x > transform.position.x && oldPosition.y == transform.position.y) //Running Left
        {
            
         
            Running();
        }
        else if (oldPosition.x == transform.position.x && oldPosition.y == transform.position.y) //Idle
        {
       
            Idle();
        }
        else if(oldPosition.x < transform.position.x && oldPosition.y == transform.position.y) //Running Right
        {           
        
            Running();
        }

        if(oldPosition.x > transform.position.x)
        {
            renderer.flipX = true;
        }
        else if(oldPosition.x < transform.position.x)
        {
            renderer.flipX = false;
        }

        oldPosition = transform.position;
    }

    void Idle()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isFalling", false);
        animator.SetBool("isJumping", false);
    }

    void Jumping()
    {
        animator.SetBool("isJumping", true);
        animator.SetBool("isFalling", false);
        animator.SetBool("isJumping", false);
    }

    void Falling()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", true);
        animator.SetBool("isJumping", false);
    }


    void Running()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isRunning", true);
        animator.SetBool("isFalling", false);
    }
}
