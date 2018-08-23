using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour {

    #region Variables
    public Rigidbody playerRigid;
    public float speed = 4f;
    public float grav = 20f;
    public float jumpSpeed = 8f;
    public float rayDistance = 1f;
    #endregion
    // Use this for initialization
    void Start () {
        playerRigid = GameObject.Find("Player").GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		float inputH = Input.GetAxis("Horizontal") * speed;

        Vector3 moveDir = new Vector3(inputH, 0, 0);
        Vector3 force = new Vector3(moveDir.x, playerRigid.y, 0);

        if(Input.GetButton("Jump") && isGrounded())
        {
            force.y = jumpSpeed;
        }

        playerRigid.velocity = force;
    }

    bool isGrounded()
    {
        Ray groundRay = new Ray(playerRigid.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(groundRay, out hit, rayDistance))
        {
            return true;
        }
        return false;
    }
}
