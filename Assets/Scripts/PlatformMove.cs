using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour {

    #region vars
    public GameObject platform;
    public GameObject playerHandle;
    public Transform pointA, pointB;
    private bool movingTo = true;
    private bool isChanging;
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

        if (movingTo)
        {
            float distance = Vector3.Distance(playerHandle.transform.position, pointB.position);
            if(distance > 0.05f)
            {
                playerHandle.transform.position = Vector3.MoveTowards(playerHandle.transform.position, pointB.position, 1f*Time.deltaTime);
            } else
            {
                isChanging = true;
                Invoke("changeBool", 1f);
            }
        } else
        {
            float distance = Vector3.Distance(playerHandle.transform.position, pointA.position);
            if (distance > 0.05f)
            {
                playerHandle.transform.position = Vector3.MoveTowards(playerHandle.transform.position, pointA.position, 1f*Time.deltaTime);
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
