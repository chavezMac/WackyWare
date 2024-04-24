using UnityEngine;
using UnityEngine.UIElements;

public class ExampleMinigame1Logic : MonoBehaviour
{
    private bool debug = true;
    //This minigame simply has the win trigger in the onClick event of the button
    void Update()
    {
        if (debug && Input.GetKeyDown(KeyCode.E))//DEBUG TESTING WIN CONDITION
        {
            MinigameBroadcaster.MinigameCompleted();
        }
        if (debug && Input.GetKeyDown(KeyCode.R))//DEBUG TESTING LOSE CONDITION
        {
            MinigameBroadcaster.MinigameFailed();
        }
        
        //Check if time has run out, and if so, we fail the minigame
        if (MainGameController.timeRemaining <= 0)
        {
            MinigameBroadcaster.MinigameFailed();
        }
    }

    public void Win(GameObject button)
    {
        MinigameBroadcaster.MinigameCompleted();
        Destroy(button);
    }
}
