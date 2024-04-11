using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameBroadcaster : MonoBehaviour
{
    private static MainGameController gameController;
    private void Start()
    {
        gameController = FindObjectOfType<MainGameController>();
    }

    public static void MinigameCompleted()
    {
        Debug.Log("The minigame reports: Level completed!");
        //Tell the gameController that the level was completed.
        gameController.MinigameDone(true);
    }

    public static void MinigameFailed()
    {
        Debug.Log("The minigame reports: Level failed");
        // Handle level failure logic here
        gameController.MinigameDone(false);
    }
}