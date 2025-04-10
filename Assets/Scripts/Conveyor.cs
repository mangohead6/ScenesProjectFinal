using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 30f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the object around the Y-axis continuously
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
