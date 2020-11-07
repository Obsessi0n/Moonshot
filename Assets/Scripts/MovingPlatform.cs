using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public bool startOn;
    private Vector3 startPosition;
    public Transform endPosition;
    private float startSpeed;
    public float speed = 1f;
    public float maxSpeed = 6f;
    public float incrementalSpeed = 0.2f;
    private float startTime;
    bool moving;

    bool movingRight=true;

    float counter = 0f;
    public float waitTime = 4f;
    // Total distance between the markers.
    private float journeyLength;

    void Start()
    {
        startSpeed = speed;
        startPosition = transform.position;
        counter = waitTime;
        if (startOn)
        {
            SetDestination(true);
            ActivatePlatform();
        }
         
    }

   
    void FixedUpdate()
    {
        if (moving)
        {
            MovePlatform();
            CheckIfReachedDestination();
        }

       
    }
    void MovePlatform()
    {


        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        if (movingRight)
        {
            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startPosition, endPosition.position, fractionOfJourney);
        }
        else
        {
            transform.position = Vector3.Lerp(endPosition.position, startPosition, fractionOfJourney);
        }
        speed += incrementalSpeed;
        // Mathf.Clamp(speed, startSpeed, maxSpeed); // Clamp doesnt work I don't know why!
        if (speed >= maxSpeed)
            speed = maxSpeed;
       

    }

    void CheckIfReachedDestination()
    {
        if (transform.position == endPosition.position && movingRight && counter <= 0f)
        {
            movingRight = false;
            SetDestination(false);
            counter = waitTime;
        }
        else if (transform.position == startPosition && !movingRight && counter <= 0f)
        {
            SetDestination(true);
            movingRight=true;
            counter = waitTime;
        }
        counter -= Time.deltaTime;
    }



    public void ActivatePlatform()
    {
        moving = true;
    }

    void SetDestination(bool right)
    {
        speed = startSpeed;
        if (right)
        {
            // Keep a note of the time the movement started.
            startTime = Time.time;
            // Calculate the journey length.
            journeyLength = Vector3.Distance(startPosition, endPosition.position);
        }
        else
        {
            // Keep a note of the time the movement started.
            startTime = Time.time;
            // Calculate the journey length.
            journeyLength = Vector3.Distance(endPosition.position, startPosition);
        }
    }
    

    //So that the player get go allong with the platform
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            collision.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = null;
        //float impulse = speed;
        //if (!movingRight)
        //    impulse = -impulse;
        //collision.transform.GetComponent<Rigidbody2D>().AddForce (
        //    new Vector2(/*collision.transform.GetComponent<Rigidbody2D>().velocity.x +*/
        //    impulse  *1000, 0));
        //    //collision.transform.GetComponent<Rigidbody2D>().velocity.y);
    }
}
