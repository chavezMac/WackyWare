using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour
{
    public float rotationSpeed = 20f;
    private float speedIncreaseInterval = 1f;
    private float timeSinceLastSpeedIncrease = 0f;
    private float speedIncreaseAmount = 10f;
    private bool rotateClockwise = true;
    public bool rotateCounterClockwiese = true;
    private float rotationTimer = 0f;

    // Update is called once per frame
    void Update()
    {
      
        timeSinceLastSpeedIncrease += Time.deltaTime;

        if (timeSinceLastSpeedIncrease >= speedIncreaseInterval)
        {
         
            rotationSpeed += speedIncreaseAmount;


            timeSinceLastSpeedIncrease %= speedIncreaseInterval;
        }

        rotationTimer += Time.deltaTime;
        if ( rotationTimer >= 3f )
        {
            rotateClockwise = !rotateClockwise;
            rotationSpeed = Mathf.Abs(rotationSpeed) * (rotateClockwise ? 1f : -1f);
            rotationTimer = 0f; 
        }

 
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}