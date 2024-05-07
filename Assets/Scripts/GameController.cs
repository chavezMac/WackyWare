using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float overrideTimeLimit;

    private bool gameWon = false;
    private bool gameLost = false;
    private float currentTime = 0f;

   

    public void PlayerWon()
    {
        if (!gameWon && !gameLost)
        {
            gameWon = true;
            MinigameBroadcaster.MinigameCompleted();
        }
    }

    public void PlayerLost()
    {
        if (!gameLost && !gameWon)
        {
            gameLost = true;
            MinigameBroadcaster.MinigameFailed();
        }
    }
} 