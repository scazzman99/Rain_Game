using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour {

    #region vars
    public GameObject platform; //platform itself
    public GameObject playerHandle; //the empty gameobject use that is parent of platform
    public Transform pointA, pointB; //points the platform will travel between
    private bool movingTo = true; //bool to tell if we are moving a certain direction
    private bool isChanging; //bool to control the delay of the platform either side
    public float platformSpeed;
    #endregion
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        //NEED TO USE AN EMPTY GAME OBJECT WITH SCALE 1,1,1 TO LET THE PLAYER TRAVEL WITH PLATFORM AND RETAIN ORIGINAL SCALE
        if (isChanging)
        {
            return;
        }

        //IF the platform is moving from pointA
        if (movingTo)
        {
            //get distance between playerHandle and pointB
            float distance = Vector3.Distance(playerHandle.transform.position, pointB.position);

            //IF distance is not too close
            if(distance > 0.05f)
            {
                //Move the playerHandle towards pointB
                playerHandle.transform.position = Vector3.MoveTowards(playerHandle.transform.position, pointB.position, platformSpeed*Time.deltaTime);
            }
            //IF CLOSE
            else
            {
                //Set is changing to true to give the invoke time to execute
                isChanging = true;
                //invoke function changeBool after 1 second
                Invoke("changeBool", 1f);
            }
        }
        //The inverse of the above
        else
        {
            float distance = Vector3.Distance(playerHandle.transform.position, pointA.position);
            if (distance > 0.05f)
            {
                playerHandle.transform.position = Vector3.MoveTowards(playerHandle.transform.position, pointA.position, platformSpeed*Time.deltaTime);
            }
            else
            {
                //this isChanging stops the Invoke from being called repeatedly
                isChanging = true;
                //allow the platform to start moving again after 1 second by setting isChanging to false and reversing the direction
                Invoke("changeBool", 1f);
            }
        }
       
	}

    private void changeBool()
    {
        //IF going on direction, change to the other and set isChanging back to false to let that happen
        if (movingTo)
        {
            movingTo = false;
        }
        else
        {
            movingTo = true;
        }
        isChanging = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //set the player's parent to the platform so they move with the platform's child that holds the player
            other.transform.parent = playerHandle.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //set the player's parent to nothing to let them leave
            other.transform.parent = null;
        }
    }
}
