using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour

{
    
    public int ballonsToShootDown = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MainGameController.timeRemaining <= 0)
        {
            MinigameBroadcaster.MinigameFailed();
        }
        else if (ballonsToShootDown == 0)
        {
            MinigameBroadcaster.MinigameCompleted();
        }
    }
}
