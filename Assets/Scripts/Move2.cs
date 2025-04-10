using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 2f; // Height of the jump
    public float lookSensitivity = 1f; // Look sensitivity back to 1

    private float xRotation = 0f;

    public Transform cameraTransform;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Automatically get the main camera if not assigned
        }
    }

    void Update()
    {
        // Ground check
        isGrounded = controller.isGrounded;

        // Handle movement
        ProcessMovement();

        // Apply gravity when not grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Keeps player grounded when falling
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Handle camera rotation on right click
        ProcessLook();

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void ProcessMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Combine the movement inputs to move the player
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);
    }

    void ProcessLook()
    {
        if (Input.GetMouseButton(1)) // Right mouse button held down
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

            // Rotate the player around the Y axis (horizontal look)
            transform.Rotate(Vector3.up * mouseX);

            // Rotate the camera around the X axis (vertical look)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevents the camera from rotating too far up/down

            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    // Handle jump logic
    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Jump force calculation
    }
}