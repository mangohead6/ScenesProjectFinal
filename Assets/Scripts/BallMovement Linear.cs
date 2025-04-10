using UnityEngine;
using System.Collections;  // Make sure this namespace is included
public class JumpingBall : MonoBehaviour
{
    public float jumpHeight = 2f; // Height of the jump
    public float jumpInterval = 2f; // Interval between jumps in seconds

    private float timeSinceLastJump = 0f; // Time passed since last jump

    void Update()
    {
        // Update the time since the last jump
        timeSinceLastJump += Time.deltaTime;

        // Check if it's time to jump
        if (timeSinceLastJump >= jumpInterval)
        {
            // Start the jump
            StartCoroutine(Jump());
            timeSinceLastJump = 0f; // Reset the timer
        }
    }

    private IEnumerator Jump()
    {
        float jumpStartTime = Time.time;
        float jumpDuration = 0.5f; // Duration of the jump

        // Jump up
        while (Time.time < jumpStartTime + jumpDuration)
        {
            float height = Mathf.Lerp(0, jumpHeight, (Time.time - jumpStartTime) / jumpDuration);
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
            yield return null;
        }

        // Fall down
        while (transform.position.y > 0)
        {
            float fallHeight = Mathf.Lerp(jumpHeight, 0, (Time.time - jumpStartTime - jumpDuration) / jumpDuration);
            transform.position = new Vector3(transform.position.x, fallHeight, transform.position.z);
            yield return null;
        }

        // Make sure the ball is at ground level
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
