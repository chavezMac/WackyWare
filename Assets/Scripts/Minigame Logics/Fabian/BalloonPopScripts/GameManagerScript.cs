using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public int balloonsPopped ;
    private Animator Animator;
    public GameObject balloon;
    public bool balloonEscaped = false; 
    public Camera mainCamera;
    // Update is called once per frame
    private void Start()
    {
        BalloonMovement.onBalloonDied += BalloonDied;
        balloonEscaped = false;
        //Animator = balloon.GetComponent<Animator>();

    }

    private void OnDestroy()
    {
        BalloonMovement.onBalloonDied -= BalloonDied;
    }

    void BalloonDied()
    {
        Debug.Log("Game Manger Recieved 'EnemyDied'");
    }

    void Update()
    {
        //Check if time has run out, and if so, we fail the minigame
       if (MainGameController.timeRemaining <= 0 || balloonEscaped == true)
       {
            
            MinigameBroadcaster.MinigameFailed();
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
                        Debug.Log("HIT");
                        //balloon.GetComponent<>()
                        popBalloon(hitObject);
                    }
                }
            }
        }
        public void popBalloon(GameObject balloon)
        {
            balloonsPopped--;
            Debug.Log(balloonsPopped);
            balloon.GetComponent<Animator>().SetTrigger("gotHit");
            if (balloonsPopped <= 0)
            { 
                // MinigameBroadcaster.MinigameCompleted();
            }
        }
    }
