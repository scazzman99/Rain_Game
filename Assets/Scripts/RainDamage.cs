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
            Debug.Log("Not in cover: DAMAGE TAKEN");
        }
    }




}
