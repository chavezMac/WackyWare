using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleMinigame2Logic : MonoBehaviour
{
    private int buttonsLeft;
    
    void Start()
    {
        // Find all GameObjects with the specified tag
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Example2Button");

        // Get the count of objects with the specified tag
        buttonsLeft = objectsWithTag.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainGameController.timeRemaining <= 0)
        {
            MinigameBroadcaster.MinigameFailed();
        }
    }

    public void DecreaseCount(GameObject button)
    {
        buttonsLeft--;
        if (buttonsLeft <= 0)
        {
            MinigameBroadcaster.MinigameCompleted();
        }
        Destroy(button);
    }
}
