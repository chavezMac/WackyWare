using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public MinigameBroadcaster Minigame;
    private bool timeUp = false;
    private bool gameFinished = false;

    void Start()
    {
        Minigame = FindObjectOfType<MinigameBroadcaster>();
    }
    void Update()
    {
        //Check if time has run out, and if so, we fail the minigame
        if (MainGameController.timeRemaining <= 0 && !MainGameController.timerPaused && !gameFinished)
        {
            MinigameBroadcaster.MinigameFailed();
            gameFinished = true;
            timeUp = true;
        }
    }


    void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                MinigameBroadcaster.MinigameFailed();
                
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("WinTrigger"))
            {
                MinigameBroadcaster.MinigameCompleted();
                
            }
        }

    void outOfTime()
    {
        MinigameBroadcaster.MinigameFailed();
        timeUp = true;

    }
    
}
