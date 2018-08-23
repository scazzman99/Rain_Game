using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRescale : MonoBehaviour {


    #region Vars
    public Rigidbody playerRigid;
    public Vector3 originalScale;
    #endregion
    // Use this for initialization
    void Start () {

        playerRigid = GameObject.Find("Player").GetComponent<Rigidbody>();
        originalScale = playerRigid.transform.localScale;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Rescale(float playerHP, float maxHp)
    {
        playerRigid.transform.localScale = originalScale * (playerHP / maxHp );
    }
}
