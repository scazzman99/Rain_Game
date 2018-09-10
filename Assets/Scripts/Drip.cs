using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drip : MonoBehaviour {

    public float vel;
    public Vector3 dir; //direction droplet will travel
    public GameObject drip; //droplet object that we will clone
    public RainDamage playerHP; //script that manages player health
    public GameObject dripSpawn; //point that droplet spawns from
    public bool isDrip; //dictates if droplet can be made
    public float dripRate; //rate at which droplets will be created
    


	// Use this for initialization
	void Start () {
        dir = Vector3.down;
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<RainDamage>();
    }
	
	// Update is called once per frame
	void Update () {
        //If dripping
        if (isDrip)
        {
            //create a droplet and get its rigid body
            GameObject waterDrop = Instantiate(drip, dripSpawn.transform.position, dripSpawn.transform.rotation);
            Rigidbody waterDropR = waterDrop.GetComponent<Rigidbody>();
            //let the water droplet fall downwards
            waterDropR.AddForce(dir);
            //set drip to false
            isDrip = false;
            //wait the duration of drip rate to release another droplet
            StartCoroutine(DropWaterSet());
        }

	}

    public IEnumerator DropWaterSet()
    {
        yield return new WaitForSecondsRealtime(dripRate);
        isDrip = true;
    }

    
}
