using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour {


	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RainDamage playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<RainDamage>();
            playerHp.playerSize -= 50f;
            Destroy(gameObject);
            
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
