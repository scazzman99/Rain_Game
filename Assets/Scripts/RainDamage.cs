using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDamage : MonoBehaviour
{

    #region Vars
    public Rigidbody playerRigid; //players rigidbody
    public float playerSize = 100f; //players current HP
    public float maxSize = 100f; //players maximum size
    //public float excessSize = 150f;
    public float rainDamage = 4f; //damage the rain will do the player
                                  // public float excessBurnRate = 6f;
    public bool inCover; //are we in cover?
                         // public bool isExcess; //is the player over 100% hp
    public CharacterRescale changeSize; //gets the class of CharacterRescale so we can call resize

    #endregion
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //will check for anything above the player
        Ray roofCheck = new Ray(playerRigid.position, Vector3.up);
        RaycastHit hit;

        //IF the new ray hits anything above us then we are in cover Else not
        if (Physics.Raycast(roofCheck, out hit, 1000f))
        {
            inCover = true;
        }
        else
        {
            inCover = false;
        }
    }




    private void LateUpdate()
    {
        //IF not in cover
        if (inCover == false)
        {
            //Deplete Size over time
            playerSize -= rainDamage * Time.deltaTime;
            //call function in another script to scale down the player size
            changeSize.Rescale(playerSize, maxSize);
            Debug.Log("Not in cover: DAMAGE TAKEN");
        }

        //IF the player size droped beneath 0
        if (playerSize < 0)
        {
            playerSize = 0; //stop the health from going beneath 0
        }

        //IF the player size is 0
        if (playerSize == 0)
        {
            //deactivate the player on death
            playerRigid.gameObject.SetActive(false);
        }
        



        /*  if (isExcess)
          {
              playerSize -= excessBurnRate * Time.deltaTime;
              if(playerSize < maxSize)
              {
                  playerSize = maxSize;
              }

              if(playerSize == maxSize)
              {
                  isExcess = false;
                  ChangeStats();
              }
              changeSize.Rescale(playerSize, maxSize);
              Debug.Log("Burning Excess");
          }
          */
    }

    private void OnTriggerStay(Collider other)
    {

        //If the player is near a fire
        if (other.CompareTag("FireZone"))
        {

            /*if (fireEffect.isPlaying == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    fireEffect.Stop();
                    playerSize += 50f;
                    if(playerSize > maxSize)
                    {
                        if(playerSize > excessSize)
                        {
                            playerSize = excessSize;
                        }
                        isExcess = true;
                        ChangeStats();
                    }
                    changeSize.Rescale(playerSize, maxSize);
                    

                }
                */

            //Increase the player health over time
            playerSize += 30 * Time.deltaTime;
            
            //IF the players size is greater than the max size
            if (playerSize > maxSize)
            {
                //set player size to the maxSize
                playerSize = maxSize;
            }
            //rescale the player based on current size and maxSize
            changeSize.Rescale(playerSize, maxSize);

        }
    }

    //OLD METHOD FOR EXCESS HEAT SYSTEM

    /*public void ChangeStats()
    {
        if (isExcess)
        {
            this.GetComponent<CharacterMovement>().speed = 8f;
            this.GetComponent<CharacterMovement>().jumpSpeed = 12f;
        } else
        {
            this.GetComponent<CharacterMovement>().speed = 4f;
            this.GetComponent<CharacterMovement>().jumpSpeed = 8f;
        }
    }
    */






}
