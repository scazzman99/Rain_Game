using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDamage : MonoBehaviour {

    #region Vars
    public Rigidbody playerRigid;
    public float playerSize = 100f;
    public float maxSize = 100f;
    public float rainDamage = 4f;
    public bool inCover;
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
    }




}
