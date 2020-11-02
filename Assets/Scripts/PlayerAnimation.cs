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
    }
 
}
