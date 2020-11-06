using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    public bool startOpen = false;

    Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
        if (startOpen)
        {
            OpenGate();
        }
    }
    public void OpenGate()
    {
        transform.position = new Vector3(startPosition.x, startPosition.y + 3f, startPosition.z);
    }

    public void CloseGate()
    {
        transform.position = startPosition;
    }
}
