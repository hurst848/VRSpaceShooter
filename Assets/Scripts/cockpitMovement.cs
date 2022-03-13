using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cockpitMovement : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    
    public GameObject cockpit;
    public Collider leftBounds;
    public Collider rightBounds;

    private Collider collsionDetection;
    private bool moveLeft = false;  // negative x
    private bool moveRight = false; // positive x

    private bool leftReached = false;
    private bool rightReached = false;

    private float xVal, yVal, zVal;
    // Start is called before the first frame update
    
    
    void Start()
    {
        xVal = cockpit.transform.position.x;
        yVal = cockpit.transform.position.y;
        zVal = cockpit.transform.position.z;
        collsionDetection = cockpit.GetComponent<Collider>();

    }



    // Update is called once per frame
    void Update()
    {
        
        xVal = cockpit.transform.position.x;
        if (moveLeft && moveRight)
        {
            moveRight = false;
            moveLeft = false;
        }
        else if (moveRight && !rightReached)
        {
            cockpit.transform.position = new Vector3(xVal + (movementSpeed * Time.deltaTime), yVal, zVal);
        }
        else if (moveLeft && !leftReached)
        {
            cockpit.transform.position = new Vector3(xVal - (movementSpeed * Time.deltaTime), yVal, zVal);
        }
        else
        {
            moveRight = false;
            moveLeft = false;
        }

        
    }

    public void switchLeft()
    {
        if (moveLeft)
        {
            moveLeft = false;
        }
        else
        {
            moveLeft = true;
        }
    }

    public void switchRight()
    {
        if (moveRight)
        {
            moveRight = false;
        }
        else
        {
            moveRight = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "rightRailBounds")
        {
            rightReached = true;
        }
        else if (other.gameObject.name == "leftRailBounds")
        {
            leftReached = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "rightRailBounds")
        {
            rightReached = false;
        }
        else if (other.gameObject.name == "leftRailBounds")
        {
            leftReached = false; 
        }
    }
}
