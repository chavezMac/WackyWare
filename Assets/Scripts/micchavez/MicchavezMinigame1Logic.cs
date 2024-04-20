using UnityEngine;

public class MicchavezMinigame1Logic : MonoBehaviour
{
    //This minigame simply has the win trigger in the onClick event of the button
    void Update()
    {
        //Check if time has run out, and if so, we fail the minigame
        if (MainGameController.timeRemaining <= 0)
        {
            MinigameBroadcaster.MinigameFailed();
        }
    }
}
