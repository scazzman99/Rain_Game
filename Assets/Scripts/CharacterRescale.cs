using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRescale : MonoBehaviour {


    #region Vars
    public Rigidbody playerRigid; //Players rigidbody
    public Vector3 originalScale; //Scale of the player upon starting
    public Vector3 currentScale; //current scale of the player
   
    #endregion
    // Use this for initialization
    void Start () {

        playerRigid = GameObject.Find("Player").GetComponent<Rigidbody>();
        originalScale = playerRigid.transform.localScale;
        //set the current scalet to original
        currentScale = originalScale;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Rescale(float playerHP, float maxHp)
    {
  
        //Get vector3 of new scale based on playerHP and MaxHP value given
        Vector3 targetScale = originalScale * ((playerHP + 50f)  / (maxHp + 50f)); //added in 50f to make it so character doesnt turn invisably small on death

        //set the rigidbody scale to the new Vector3 scale
        playerRigid.transform.localScale = targetScale;
        currentScale = targetScale;

    }

   
}
