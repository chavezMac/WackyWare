using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour
{
    public float rotationSpeed = 30f;
    private float speedIncreaseInterval = 2f;
    private float timeSinceLastSpeedIncrease = 0f;
    private float speedIncreaseAmount = 15f;
    private bool rotateClockwise = true;
    private float rotationTimer = 0f;

    void Update()
    {
        // Increase rotation speed at regular intervals
        timeSinceLastSpeedIncrease += Time.deltaTime;
        if (timeSinceLastSpeedIncrease >= speedIncreaseInterval)
        {
            rotationSpeed += speedIncreaseAmount;
            timeSinceLastSpeedIncrease -= speedIncreaseInterval; // Adjust timer
        }

        // Change rotation direction after a certain time
        rotationTimer += Time.deltaTime;
        if (rotationTimer >= 2f)
        {
            rotateClockwise = !rotateClockwise;
            rotationSpeed = Mathf.Abs(rotationSpeed) * (rotateClockwise ? 1f : -1f);
            rotationTimer = 0f;
        }

        // Rotate the object
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}