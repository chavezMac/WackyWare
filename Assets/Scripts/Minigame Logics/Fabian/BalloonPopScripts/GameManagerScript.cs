using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //public int balloonsPopped ;
    private BalloonMovement ballMovement;
    private Animator Animator;
    public GameObject balloon;
    public bool balloonEscaped;
    public Camera mainCamera;

    private void Start()
    {
        balloonEscaped = false;
        BalloonMovement.onBalloonDied += BalloonDied;
        
    }

    private void OnDestroy()
    {
        BalloonMovement.onBalloonDied -= BalloonDied;
    }

    void BalloonDied()
    {
        Debug.Log("GameManager Received 'BalloonDied' event");
    }

    void Update()
    {
        Debug.Log(MainGameController.timeRemaining);    
        if (MainGameController.timeRemaining <= 0)
        {
            MinigameBroadcaster.MinigameCompleted();
        }
        
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 rayPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

            if(hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.CompareTag("Balloon"))
                {
                    Debug.Log("Balloon clicked");
                    popBalloon(hitObject);
                }
            }
        }
    }

    public void popBalloon(GameObject balloon)
    {
        Debug.Log("Balloon popped");
        StartCoroutine(balloon.GetComponent<BalloonMovement>().Vanish());
        balloon.GetComponent<AudioSource>().Play();

    }
}