using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drip : MonoBehaviour {

    public float vel;
    public Vector3 dir;
    public GameObject drip;
    public RainDamage playerHP;
    public GameObject dripSpawn;
    public bool isDrip;
    public float dripRate;
    


	// Use this for initialization
	void Start () {
        dir = Vector3.down;
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<RainDamage>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isDrip)
        {
            GameObject waterDrop = Instantiate(drip, dripSpawn.transform.position, dripSpawn.transform.rotation);
            Rigidbody waterDropR = waterDrop.GetComponent<Rigidbody>();
            waterDropR.AddForce(dir);
            isDrip = false;
            StartCoroutine(DropWaterSet());
        }

	}

    public IEnumerator DropWaterSet()
    {
        yield return new WaitForSecondsRealtime(dripRate);
        isDrip = true;
    }

    
}
