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

        Vector3 moveDir = new Vector3(inputH, 0, 0); //only allow general movement along x-axis
        Vector3 force = new Vector3(moveDir.x, playerRigid.velocity.y, 0); //translate this to force for the rigid body

        if(Input.GetButton("Jump") && isGrounded())
        {
            force.y = jumpSpeed; //add Y componenet to the force for jumping on rigid body
        }

        playerRigid.velocity = force; //translate the force to the rigid body in form of movement

        if(moveDir.magnitude > 0)
        {
            playerRigid.rotation = Quaternion.LookRotation(moveDir); //rotate in direction of movement vector
        }
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

    private void OnDrawGizmos()
    {
        Ray groundRay = new Ray(playerRigid.position, Vector3.down);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundRay.origin, groundRay.origin + groundRay.direction * rayDistance);

    }


}
