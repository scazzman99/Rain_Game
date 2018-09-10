using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletDamage : MonoBehaviour {

    public RainDamage playerHP;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //need to define playerHP in here on touch else null is thrown on player death
            playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<RainDamage>();
            playerHP.playerSize -= 50f;
            playerHP.changeSize.Rescale(playerHP.playerSize, playerHP.maxSize);
            Destroy(gameObject);

        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
