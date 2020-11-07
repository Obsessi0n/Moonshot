using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class PlayerRecall : MonoBehaviour
{

    bool isRewinding = false;
    bool isRecording = false;

    public float recordTime = 10f;
    List<Vector3> positions;

    public GameObject playerHologram;

    public LayerMask Props;


    void Start()
    {
        positions = new List<Vector3>();
    }

    
    void Update()
    {
        


        if (Input.GetKeyDown(KeyCode.Return) && isRecording && CheckHologramPCCollision())
        {
            isRecording = false;
            StartRewind();
        }

        else if (Input.GetKeyDown(KeyCode.Return) && CheckHologramPCCollision())
        {
            isRecording = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        Collider2D colliders = Physics2D.OverlapCircle(transform.position, 1f, Props);

        if (colliders != null)
        {

            if (colliders.name == "Portal")
            {

                colliders.GetComponent<Portal>().Teleport();
            }

        }
    }

    bool CheckHologramPCCollision()
    {
    Collider2D colliders = Physics2D.OverlapCircle(transform.position, 1f, Props);
    
    if(colliders != null)
    {
        
        if (colliders.name == "HologramPC")
        {
            return true;

        }
       
    }
    return false;
    }

    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else if(isRecording)
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
       
        playerHologram.SetActive(true);
        isRewinding = true;
    }

    void StopRewind()
    {
        isRewinding = false;
        playerHologram.SetActive(false);
    }



}
