using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour
{
    public float rotationSpeed = 15f;
    private float speedIncreaseInterval = 2f;
    private float timeSinceLastSpeedIncrease = 0f;
    private float speedIncreaseAmount = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
        timeSinceLastSpeedIncrease += Time.deltaTime;

        
        if (timeSinceLastSpeedIncrease >= speedIncreaseInterval)
        {
           
            rotationSpeed += speedIncreaseAmount;

          
            timeSinceLastSpeedIncrease = 0f;
        }

      
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}