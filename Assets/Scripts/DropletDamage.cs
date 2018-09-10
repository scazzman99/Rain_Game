using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletDamage : MonoBehaviour {

    public RainDamageHpHandler playerHP;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //need to define playerHP in here on touch else null is thrown on player death
            playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<RainDamageHpHandler>();
            //take 50 from player health
            playerHP.playerSize -= 50f;
            //call the rescale script
            playerHP.changeSize.Rescale(playerHP.playerSize, playerHP.maxSize);
            //destroy the droplet
            Destroy(gameObject);

        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
