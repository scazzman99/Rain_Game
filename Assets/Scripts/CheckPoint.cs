using UnityEngine;
using System.Collections;

[AddComponentMenu("FirstPerson/Checkpoint")]
public class CheckPoint : MonoBehaviour
{
    #region Variables
    [Header("Check Point Elements")]

    //GameObject for our currentCheck
    public GameObject curCheckpoint;

    [Header("Character Handler")]

    //character handler script that holds the players health
    //public healthbar charH;

    //This line would grab the current code that handles health directly
    public RainDamageHpHandler charHP;
    #endregion

    #region Start


    private void Start()
    {

        //the character handler is the component attached to our player
        //charH = this.GetComponent<healthbar>();
        charHP = GetComponent<RainDamageHpHandler>();
        #region Check if we have Key
        if (PlayerPrefs.HasKey("SpawnPoint"))
        {
            //then our checkpoint is equal to the game object that is named after our save file
            curCheckpoint = GameObject.Find(PlayerPrefs.GetString("SpawnPoint"));
            //our transform.position is equal to that of the checkpoint
            transform.position = curCheckpoint.transform.position;
        }
        #endregion
    }
    #endregion
    #region Update
    private void Update()
    {

        //if our characters health is less than or equal to 0
       /* if (charH.curHealth == 0)
        {
            //our transform.position is equal to that of the checkpoint
            transform.position = curCheckpoint.transform.position;

            //our characters health is equal to full health
            charH.curHealth = charH.maxHealth;

            //character is alive
            charH.alive = true;

            //characters controller is active	
            //charH.controller.enabled = true;

           


        }
        */

        //This if statement is in terms of the RainDamageHPHandler, previous if statement ended up locking the player in position for some reason
        if(charHP.playerSize == 0)
        {
            transform.position = curCheckpoint.transform.position;

            charHP.playerSize = charHP.maxSize;

            charHP.gameObject.SetActive(true);
        }


    }
    #endregion
    #region OnTriggerEnter (Collider other)
    private void OnTriggerEnter(Collider other)
    {
        //if our other objects tag when compared is CheckPoint
        if (other.CompareTag("CheckPoint"))
        {
            //our checkpoint is equal to the other object
            curCheckpoint = other.gameObject;
            //save our SpawnPoint as the name of that object
            PlayerPrefs.SetString("SpawnPoint", curCheckpoint.name);

        }

    }
    #endregion
}

