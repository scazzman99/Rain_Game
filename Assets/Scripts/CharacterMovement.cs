using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour {

    #region Variables
    public Rigidbody playerRigid;
    public float speed = 4f; //adjusts speed of the player
    public float grav = 20f; //adjusts the gravity value
    public float jumpSpeed = 8f; //adjust the jump speed of the player
    public float rayDistance = 1f; //distance of the ray that dictates if we can jump. Needs resizing with scale later
    public float dashSpeed = 10f; //adjusts the speed of the players dashing
    public Vector3 dashDir; //direction of the dash
    public bool isDashing; //are we dashing?
    public RainDamageHpHandler playerHP;
    #endregion
    // Use this for initialization
    void Start () {
        playerRigid = GameObject.Find("Player").GetComponent<Rigidbody>();
        dashDir = Vector3.zero; //set the initial dash direction to zero
        playerHP = GetComponent<RainDamageHpHandler>();

	}
	
	// Update is called once per frame
	void Update () {

        //IF we are dashing right now
        if (isDashing)
        {
            //players velocity is dashDir value as it has already been set
            playerRigid.velocity = dashDir;

            //change the playerSize value in the RainDamage script attached to this object (the player)
            GetComponent<RainDamageHpHandler>().playerSize -= 8f * Time.deltaTime;

            //rescale the player based on the change above
            GetComponent<CharacterRescale>().Rescale(GetComponent<RainDamageHpHandler>().playerSize, GetComponent<RainDamageHpHandler>().maxSize);

            //IF the shiftKey is not being pressed
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                //Stop dashing
                isDashing = false;
                //gives opposing force to help stop the player in the air
                playerRigid.AddForce(-dashDir * 0.8f);
            }
        }
        else
        {
            //horizontal movement is getAxis horizontal multiplied by our speed
            float inputH = Input.GetAxis("Horizontal") * speed;
            //vertical DIRECTION is held here, will only yeild a proper value if dashing
            float dirV = Input.GetAxis("Vertical");

            //only allow general movement along x-axis. Allow for y input that only counts when dashing
            Vector3 moveDir = new Vector3(inputH, dirV, 0);

            //translate this to force for the rigid body
            Vector3 force = new Vector3(moveDir.x, playerRigid.velocity.y, 0);

            //IF we hit jump button and we are grounded
            if (Input.GetButton("Jump") && isGrounded())
            {
                //add Y componenet to the force for jumping on rigid body that is from the jump
                force.y = jumpSpeed;
            }

            //IF left shift is pressed and we arent already holding dash
            if (Input.GetKey(KeyCode.LeftShift) && moveDir != Vector3.zero)
            {
                //Set moveDir to the direction player was inputing at time of the dash. Use dash speed
                moveDir = new Vector3(Input.GetAxis("Horizontal") * dashSpeed, Input.GetAxis("Vertical") * dashSpeed, 0);
                //add the new force to the rigid body
                force = new Vector3(moveDir.x, moveDir.y, 0);
                //dashdir is force
                dashDir = force;

                //add an impulse to the rigid body to start the dashing
                playerRigid.AddForce(dashDir * 2f, ForceMode.Impulse);

                //takes more fuel to start the dash than maintain it
                GetComponent<RainDamageHpHandler>().playerSize -= 9f;
                //rescale the player based on the health they lost
                GetComponent<CharacterRescale>().Rescale(GetComponent<RainDamageHpHandler>().playerSize, GetComponent<RainDamageHpHandler>().maxSize);
                //we are now dashing
                isDashing = true;

                //return so that we do not override our velocity
                return;
            }

            playerRigid.velocity = force; //translate the force to the rigid body in form of movement

        }
    }

    bool isGrounded()
    {
        //create a new ray from the players position downwards
        Ray groundRay = new Ray(playerRigid.position, Vector3.down);
        RaycastHit hit;
        //IF the raycast hits anything then we must be grounded
        if (Physics.Raycast(groundRay, out hit, rayDistance))
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

    private void OnTriggerEnter(Collider other)
    {
        //IF we touched a deathpit then set player active to false
        if (other.CompareTag("DeathPit"))
        {
            
            playerRigid.gameObject.SetActive(false);
            Debug.Log("You have been extinguished");

        }
        //ELSE IF we touched destroyable platform e.g. wood, then destroy the wood with the coroutine and recreate it with the coroutine
        else if (other.CompareTag("Wood"))
        {
            Debug.Log("Platform to destroy");
            StartCoroutine(DestroyPlatform(other.gameObject));
            StartCoroutine(RecreatePlatform(other.gameObject));
        }
    }

    //Coroutine to run to destroy a platform after it has been touched
   IEnumerator DestroyPlatform(GameObject platform)
    {
        yield return new WaitForSecondsRealtime(1f);
        platform.SetActive(false);
        

    }

    //Coroutine to recreate the platform that had been destroyed
    IEnumerator RecreatePlatform(GameObject platform)
    {
        yield return new WaitForSecondsRealtime(5f);
        platform.SetActive(true);
    }


}
