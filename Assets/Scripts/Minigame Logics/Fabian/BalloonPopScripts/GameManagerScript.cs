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
    public bool isVanished = false;

    private void Start()
    {
        Invoke("startMusic", .7f);
        balloonEscaped = false;
        BalloonMovement.onBalloonDied += BalloonDied;

    }

    private void OnDestroy()
    {
        BalloonMovement.onBalloonDied -= BalloonDied;
    }

    void BalloonDied()
    {
        //Debug.Log("GameManager Received 'BalloonDied' event");
    }

    void Update()
    {
        // Check if the timer has run out and no balloons have escaped
        if (MainGameController.timeRemaining <= 0 && !balloonEscaped)
        {
            GetComponent<AudioSource>().Stop();
            MinigameBroadcaster.MinigameCompleted();
        }
        else if (balloonEscaped)
        {
            GetComponent<AudioSource>().Stop();
            MinigameBroadcaster.MinigameFailed();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 rayPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.CompareTag("Balloon"))
                {
                    // Debug.Log("Balloon clicked");
                    popBalloon(hitObject);
                    if (isVanished)
                    {
                        Destroy(hitObject);
                        isVanished = false;
                    }
                }


            }
        }
    }

    public void popBalloon(GameObject balloon)
    {
        //Debug.Log("Balloon popped");
        StartCoroutine(balloon.GetComponent<BalloonMovement>().Vanish());
        balloon.GetComponent<AudioSource>().Play();
    }

    void startMusic()
    {
        GetComponent<AudioSource>().Play();
    }

}