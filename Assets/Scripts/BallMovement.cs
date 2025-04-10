using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public float radius = 5f; // The radius of the circle
    public float speed = 2f;  // The speed at which the ball moves

    private float angle = 0f; // Starting angle

    void Update()
    {
        // Update the angle based on time and speed
        angle += speed * Time.deltaTime;

        // Calculate the new position using sine and cosine for circular motion
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // Apply the new position
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
