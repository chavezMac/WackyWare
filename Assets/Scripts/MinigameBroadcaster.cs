using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameBroadcaster : MonoBehaviour
{
    private MainGameController gameController;
    private void Start()
    {
        gameController = FindObjectOfType<MainGameController>();
    }

    public void LevelCompleted()
    {
        Debug.Log("Level completed!");
        //Tell the gameController that the level was completed.
        gameController.LevelCompleted();
    }

    public void LevelFailed()
    {
        Debug.Log("Level failed");
        // Handle level failure logic here
    }
}