using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{

    public MinigameBroadcaster Minigame;
    public float overrideTimeLimit;
    private float currentTime = 0f;
    private bool timeUp = false;
    private bool gameFinished = false;
    public bool gamePaused = false;
   
    
    void Start()
    {
        Minigame = FindObjectOfType<MinigameBroadcaster>();
    }
    void Update()
    {

        if (!gameFinished && currentTime >= Minigame.overrideTimeLimit)
        {

            MinigameBroadcaster.MinigameCompleted();
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
            currentTime = 0f; 
        }
    }

    void outOfTime()
    {
        MinigameBroadcaster.MinigameCompleted();
        timeUp = true;

    }

}

