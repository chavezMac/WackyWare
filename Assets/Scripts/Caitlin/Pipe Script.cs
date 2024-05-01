using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] rotations = {0, 90, 180, 270};
    private float correctRotation = 0f;
    private int correctPipes = 0; 
    private Game2GameManager gameManager; 

    [SerializeField]
    bool isPlaced = false;

    private void Start()
    {
        gameManager = FindObjectOfType<Game2GameManager>(); 
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

        if (transform.eulerAngles.z == correctRotation)
        {
            isPlaced = true;
            correctPipes = correctPipes + 1;
            Debug.Log("Correct = " + correctPipes);
            gameManager.CheckCorrectPipes(correctPipes); 
        }
    }

    private void OnMouseDown()
{
    transform.Rotate(new Vector3(0, 0, 90));

    float currentAngle = transform.eulerAngles.z;

    // Check if the current angle is within a tolerance range of the correct rotation
    if (Mathf.Approximately(currentAngle, correctRotation))
    {
        if (!isPlaced) // Ensure it's not already placed before incrementing
        {
            correctPipes++; // Increment correctPipes only when not already placed
            isPlaced = true;
            Debug.Log("Correct = " + correctPipes);
        }
    }
    else
    {
        if (isPlaced) // If it was previously placed and now rotated incorrectly, decrement
        {
            correctPipes--;
            isPlaced = false;
            Debug.Log("Correct = " + correctPipes);
        }
    }

    gameManager.CheckCorrectPipes(correctPipes);
}

    // Update is called once per frame
    void Update()
    {
    }
}