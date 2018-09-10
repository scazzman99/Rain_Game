using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbar : MonoBehaviour {

    #region Variables
    [Header("Character")]

    #region Character 
    public bool alive;
    public CharacterController controller;
    #endregion
    [Header("Health")]
    #region Health
    //max and min health
    public float maxHealth;
    public float curHealth;
    public GUIStyle healthBar;
    #endregion
 
  
    #region Start
    public void Start()
    {
       
        maxHealth = 100f;
        curHealth = maxHealth;
        alive = true;
        controller = this.GetComponent<CharacterController>();


    }
    #endregion
    #region Update
    private void Update()
    {

        curHealth = (int)curHealth;
    }
    #endregion
    #region LateUpdate
    private void LateUpdate()
    {

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth < 0 || !alive)
        {
            curHealth = 0;
            Debug.Log("if less than 0 = 0");
        }
        if (alive && curHealth == 0)
        {
            alive = false;
            controller.enabled = false;
            Debug.Log("Disable on death");

        }

    }
    #endregion
    #region OnGUI
    private void OnGUI()
    {

        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 9; y++) ;
        }
       
        GUI.Box(new Rect(6 * scrW, 0.25f * scrH, 4 * scrW, 0.5f * scrH), "");
        GUI.Box(new Rect(6 * scrW, 0.25f * scrH, curHealth * (4 * scrW) / maxHealth, 0.5f * scrH), "", healthBar);
       
    }
    #endregion

    #endregion

}
