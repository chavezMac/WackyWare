using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public float timeRemaining = 10f; // Initial time remaining in seconds
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro text component

    void Update()
    {
        // Decrease the time remaining by the time elapsed since the last frame
        timeRemaining -= Time.deltaTime;

        // Update the text to display the remaining time
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
        }

        // Check if the timer has reached zero
        if (timeRemaining <= 0)
        {
            // Timer has expired
            UnityEngine.Debug.Log("Time's up!");
           
        }
    }
}