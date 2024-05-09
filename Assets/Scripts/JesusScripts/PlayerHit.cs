using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private bool timeUp = false;
    private bool gameFinished = false;
    public bool gamePaused = false;
    void Update()
    {
        //Check if time has run out, and if so, we win the minigame
        if (MainGameController.timeRemaining <= 0 && !MainGameController.timerPaused && !gameFinished)
        {
            MinigameBroadcaster.MinigameCompleted();
            gameFinished = true;
            timeUp = true;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !gameFinished)
        {
            Destroy(gameObject);

            MinigameBroadcaster.MinigameFailed();
            gameFinished = true;
        }
    }
}

