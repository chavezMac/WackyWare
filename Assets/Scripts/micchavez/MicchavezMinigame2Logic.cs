using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicchavezMinigame2Logic : MonoBehaviour
{
    //Get reference to Player
    public GameObject player;
    public GameObject finishLine;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        finishLine = GameObject.Find("Finish");
    }

    // Update is called once per frame
    void Update()
    {
        //Check if time has run out, and if so, we fail the minigame
        if (MainGameController.timeRemaining <= 0)
        {
            MinigameBroadcaster.MinigameFailed();
        }

        if (player.transform.position.z >= finishLine.transform.position.z)
        {
            MinigameBroadcaster.MinigameCompleted();
        }
    }
}
