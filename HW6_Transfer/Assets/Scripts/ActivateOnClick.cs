using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnClick : MonoBehaviour
{
    Rigidbody rb; //var for rigidbody
    Vector3 startPosition;
    public bool SimulationRunning = false;

    public int prizeCounter = 4;
    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody>(); //Get RigidBody Component
        rb.isKinematic = true; //Make it Kinematic (ie, doen't move with physics)
        startPosition = rb.transform.position;

        GameManager.instance.prizeCounter = prizeCounter; //tell the game manager how many prizes to hit this level
        GameManager.instance.resetScoreCounter(); //update the counter based on load game info
    }

    public void runBallSimulation() //if you click on the object
    {
        if (rb.isKinematic == true)
        {

            rb.isKinematic = false; //make it able to move w/ physics
        }
        else
        {
            rb.isKinematic = true;
            rb.MovePosition(startPosition);
            GameManager.instance.currentResetScore++;
            GameManager.instance.resetScoreCounter();
            
        }
    }

}
