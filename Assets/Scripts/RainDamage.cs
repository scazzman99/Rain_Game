using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDamage : MonoBehaviour {

    #region Vars
    public Rigidbody playerRigid;
    public float playerSize = 100f;
    public float maxSize = 100f;
    public float excessSize = 150f;
    public float rainDamage = 4f;
    public float excessBurnRate = 6f;
    public bool inCover;
    public bool isExcess; //is the player over 100% hp
    public CharacterRescale changeSize; //gets the class of CharacterRescale so we can call resize
    
    #endregion
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray roofCheck = new Ray(playerRigid.position, Vector3.up); //will check for anything above the player
        RaycastHit hit;
        if(Physics.Raycast(roofCheck, out hit, 1000f))
        {
            inCover = true;
        } else
        {
            inCover = false;
        }
	}




    private void LateUpdate()
    {
        if(inCover == false)
        {
            playerSize -= rainDamage * Time.deltaTime; //Deplete Size over time
            if (playerSize < 0)
            {
                playerSize = 0; //stop the health from going beneath 0
            }

            if(playerSize == 0)
            {
                playerRigid.gameObject.SetActive(false); //deactivate the player on death
            }
            changeSize.Rescale(playerSize, maxSize); //call function in another script to scale down the player size
            Debug.Log("Not in cover: DAMAGE TAKEN");
        }

        if (isExcess)
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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FireZone"))
        {
            ParticleSystem fireEffect = other.transform.Find("FireEffect").GetComponent<ParticleSystem>();
            if (fireEffect.isPlaying == true)
            {
                /*if (Input.GetKeyDown(KeyCode.E))
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
                playerSize += 30 * Time.deltaTime;
                if(playerSize >= maxSize)
                {
                    playerSize = maxSize;
                }
                changeSize.Rescale(playerSize, maxSize);
            }
        }
    }

    public void ChangeStats()
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

  




}
