using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 playerVelocity; // Represents 3D vector and points
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;


    // Start is called before the first frame update
    void Start()
    {
        //Create a Controller
        controller = gameObject.AddComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if(groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        var horizontal = Input.GetAxis("Horizontal"); // Get Horizontal Input mapped to Arrow Keys
        var vertical = Input.GetAxis("Vertical"); //Get Vertical Input mapped to Arrow Keys

        Vector3 move = new Vector3(horizontal, 0, vertical); // Vector3(float x, float y, float z);
        controller.Move(move * Time.deltaTime * playerSpeed); // Direction Vector * Time * Velocity;

        if(move != Vector3.zero)
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

    }
}
