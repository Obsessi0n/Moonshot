using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public GameObject connectedTo;

    public void Teleport()
    {
        GameObject.Find("Player").transform.position = connectedTo.transform.position;
    }

}
