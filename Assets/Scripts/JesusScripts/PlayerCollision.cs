using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public MinigameBroadcaster Minigame;
    public float overrideTimeLimit;
    private float currentTime = 0f;
    private bool timeUp = false;
    private bool gameFinished = false;

    void Start()
    {
        Minigame = FindObjectOfType<MinigameBroadcaster>();
    }
    void Update()
    {
       
        if (!gameFinished && currentTime >= Minigame.overrideTimeLimit)
        {

            MinigameBroadcaster.MinigameFailed();
            gameFinished = true; 
        }

      
        currentTime += Time.deltaTime;
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
