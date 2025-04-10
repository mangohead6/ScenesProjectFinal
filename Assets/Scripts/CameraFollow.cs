using UnityEngine;

public class CameraAndMovement : MonoBehaviour
{
    public Transform target; // The object the camera should follow (ghost)
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the target to camera
    public float smoothSpeed = 0.125f;

    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;

    public float cameraSensitivity = 3f; // Mouse sensitivity
    public float pitchMin = -20f; // Min vertical angle
    public float pitchMax = 60f; // Max vertical angle

    private Camera mainCamera;
    private float yaw = 0f; // Horizontal rotation
    private float pitch = 10f; // Vertical rotation

    void Start()
    {
        mainCamera = Camera.main;

        // Initialize yaw and pitch from current camera rotation
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = cameraForward * vertical + cameraRight * horizontal;
        if (moveDirection.magnitude > 0)
        {
            target.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            target.rotation = Quaternion.RotateTowards(target.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Right-click to rotate camera
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            yaw += Input.GetAxis("Mouse X") * cameraSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * cameraSensitivity;
            pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void LateUpdate()
    {
        // Calculate new camera position based on yaw/pitch
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.position + rotation * offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.LookAt(target);
    }
}
