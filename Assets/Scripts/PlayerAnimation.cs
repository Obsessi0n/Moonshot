using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer renderer;


    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Moving(string dir)
    {
        animator.SetBool("isRunning", true);
        if(dir != "Right")
        {
            renderer.flipX = true;
        }
        else
        {
            renderer.flipX = false;
        }
    }

    public void Idle()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", false);

    }

    public void Jumping()
    {
        animator.SetBool("isJumping", true);
        animator.SetBool("isFalling", false);
        animator.SetBool("isRunning", false);
    }

    public void Falling()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", true);
        animator.SetBool("isRunning", false);
    }

    public void NotFalling()
    {
        animator.SetBool("isFalling", false);
    }

}
