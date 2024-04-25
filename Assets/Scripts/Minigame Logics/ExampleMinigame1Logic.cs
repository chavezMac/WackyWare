using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ExampleMinigame1Logic : MonoBehaviour
{
    private bool debug = true;

    public Button button;
    //This minigame simply has the win trigger in the onClick event of the button
    void Update()
    {
        if (debug && Input.GetKeyDown(KeyCode.E))//DEBUG TESTING WIN CONDITION
        {
            MinigameBroadcaster.MinigameCompleted();
            Destroy(button);
        }
        if (debug && Input.GetKeyDown(KeyCode.R))//DEBUG TESTING LOSE CONDITION
        {
            MinigameBroadcaster.MinigameFailed();
            Destroy(button);
        }
        
        //Check if time has run out, and if so, we fail the minigame
        if (MainGameController.timeRemaining <= 0 && !MainGameController.timerPaused)
        {
            MinigameBroadcaster.MinigameFailed();
            Destroy(button);
        }
    }

    public void Win()
    {
        MinigameBroadcaster.MinigameCompleted();
        Destroy(button);
    }
}
