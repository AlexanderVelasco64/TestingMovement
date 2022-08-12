using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;

    private Vector3 playerVelocity; // Represents 3D vector and points
    private bool groundedPlayer;
    public float playerSpeed = 20f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;


    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal"); // Get Raw Horizontal Input (not smoothed to 1 or 0)
        float vertical = Input.GetAxisRaw("Vertical");   // Get Raw Vertical Input (not smoothed)
        groundedPlayer = Input.GetButtonDown("Jump"); // Get Jump
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; //Direction is a vector pointing in the direction of the horizontal and vertical inputs, normalized to have a magnitude of 1
        
        if(direction.magnitude >= 0.1f) //If direction is greater than 0.1 (ensures a deadzone)
        {
            // Get the angle of the movement, convert it to degrees (because Atan2 returns radians) and add the angle of the camera.
            // The camera angle needs to be added because the user needs to travel in the direction the camera is pointing in the y axis
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            // Get the angle after it has been smoothed from the original direction to the target direction with the speed of smooth velocity and within smooth time
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // Transform the players rotation but the amount calculated 
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Get direction the player will be moving 
            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

            // Move player in direction, normalized to 1, multiplied by speed and time 
            // m/s * s = m  speed * time = distance traveled
            controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
        }

        if(groundedPlayer == true){
            direction.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        direction.y += gravityValue * Time.deltaTime;
        controller.Move(direction * Time.deltaTime);
        

        /*

        // Determine if player is in the air
        groundedPlayer = controller.isGrounded;

        // If player is grounded and not currently falling or rising
        if(groundedPlayer && playerVelocity.y < 0.1)
        {
            playerVelocity.y = 0f; // Set player vertical speed to zero
        }

        var horizontal = Input.GetAxis("Horizontal"); // Get Horizontal Input mapped to Arrow Keys
        var vertical = Input.GetAxis("Vertical"); //Get Vertical Input mapped to Arrow Keys

        Vector3 move = new Vector3(horizontal, 0, vertical); // Vector3(float x, float y, float z);
        controller.Move(move * Time.deltaTime * playerSpeed); // Direction Vector * Time * Velocity;

        if(move != Vector3.zero) // If player is not moving
        {
            gameObject.transform.forward = move;
        }

        //Changes the height position of the player
        if(Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue); //
        }

        playerVelocity.y += gravityValue * Time.deltaTime; // Add Velocity to players vertical
        controller.Move(playerVelocity * Time.deltaTime);  // Moves player to match velocity

        */

    }
}
