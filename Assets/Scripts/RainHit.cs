using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RainHit : MonoBehaviour {

    #region Vars
    
    #endregion
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RainCover"))
        {
            
        }
    }


}
