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
    public float dashSpeed = 10f;
    public Vector3 dashDir;
    public bool isDashing;
    #endregion
    // Use this for initialization
    void Start () {
        playerRigid = GameObject.Find("Player").GetComponent<Rigidbody>();
        dashDir = Vector3.zero;

	}
	
	// Update is called once per frame
	void Update () {

        if (isDashing)
        {
            playerRigid.velocity = dashDir;
            GetComponent<RainDamage>().playerSize -= 8f * Time.deltaTime;
            GetComponent<CharacterRescale>().Rescale(GetComponent<RainDamage>().playerSize, GetComponent<RainDamage>().maxSize);
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                isDashing = false;
                //Vector3 counterForce = new Vector3(dashDir.x * -0.5f, dashDir.y * -0.5f, 0);
                //playerRigid.velocity = dashDir;
            }
        }
        else
        {

            float inputH = Input.GetAxis("Horizontal") * speed;
            float dirV = Input.GetAxis("Vertical");


            Vector3 moveDir = new Vector3(inputH, dirV, 0); //only allow general movement along x-axis. Allow for y input that only counts when dashing
            Vector3 force = new Vector3(moveDir.x, playerRigid.velocity.y, 0); //translate this to force for the rigid body

            if (Input.GetButton("Jump") && isGrounded())
            {
                force.y = jumpSpeed; //add Y componenet to the force for jumping on rigid body
            }

            if (Input.GetKey(KeyCode.LeftShift) && moveDir != Vector3.zero)
            {
                moveDir = new Vector3(Input.GetAxis("Horizontal") * dashSpeed, Input.GetAxis("Vertical") * dashSpeed, 0);
                force = new Vector3(moveDir.x, moveDir.y, 0);
                dashDir = force;
                playerRigid.AddForce(dashDir, ForceMode.Impulse);
                //takes more fuel to start the dash than maintain it?
                GetComponent<RainDamage>().playerSize -= 5f;
                GetComponent<CharacterRescale>().Rescale(GetComponent<RainDamage>().playerSize, GetComponent<RainDamage>().maxSize);
                isDashing = true;
                return;
            }

            playerRigid.velocity = force; //translate the force to the rigid body in form of movement

            /* if(moveDir.magnitude > 0)
             {
                 playerRigid.rotation = Quaternion.LookRotation(moveDir); //rotate in direction of movement vector
             }
             */
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathPit"))
        {
            
            playerRigid.gameObject.SetActive(false);
            Debug.Log("You have been extinguished");

        } else if (other.CompareTag("Wood"))
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
