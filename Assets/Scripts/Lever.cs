using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public void Activate()
    {
        if (GetComponent<Animator>().GetBool("On")){
            Deactivate();
            return;
        }
        GetComponent<Animator>().SetBool("On", true);
    }

    public void Deactivate()
    {
        GetComponent<Animator>().SetBool("On", false);
    }
}
