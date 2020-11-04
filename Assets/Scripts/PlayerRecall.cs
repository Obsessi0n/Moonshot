using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class PlayerRecall : MonoBehaviour
{

    bool isRewinding = false;

    public float recordTime = 10f;
    List<Vector3> positions;

    public GameObject playerHologram;

    void Start()
    {
        positions = new List<Vector3>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartRewind();
        //if (Input.GetKeyUp(KeyCode.Return))
         //   StopRewind();
    }

    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void Rewind()
    {
        if(positions.Count > 0)
        {
            playerHologram.transform.position = positions[positions.Count - 1];
            positions.RemoveAt(positions.Count - 1);
        }
        else
        {
            StopRewind();
        }

    }

    void Record()
    {
        if(positions.Count > Mathf.Round(recordTime * 1f / Time.fixedDeltaTime))
        {
            positions.RemoveAt(positions.Count - 1);
        }

        positions.Insert(0, transform.position);
    }

    void StartRewind()
    {
        transform.position = new Vector3(-6.36000013f, 0, 0);
        playerHologram.SetActive(true);
        isRewinding = true;
    }

    void StopRewind()
    {
        isRewinding = false;
        playerHologram.SetActive(false);
    }
}
