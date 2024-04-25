using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameBroadcaster : MonoBehaviour
{
    private static MainGameController gameController;
    private static bool gameFinished = false;
    private void Start()
    {
        gameController = FindObjectOfType<MainGameController>();
        gameFinished = false;
    }

    public static void MinigameCompleted()
    {
        if (!gameFinished)
        {
            Debug.Log("The minigame reports: Level completed!");
            gameController.MinigameDone(true);
            gameFinished = true;
        }
    }

    public static void MinigameFailed()
    {
        if (!gameFinished)
        {
            Debug.Log("The minigame reports: Level failed");
            gameController.MinigameDone(false);
            gameFinished = true;
        }
    }
}