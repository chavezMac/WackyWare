using UnityEngine;

public class ExampleMinigame2Logic : MonoBehaviour
{
    private int buttonsLeft;
    private bool debug = true;
    
    void Start()
    {
        // Find all GameObjects with the specified tag
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Example2Button");

        // Get the count of objects with the specified tag
        buttonsLeft = objectsWithTag.Length;
    }

    // Update is called once per frame
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

    public void DecreaseCount(GameObject button)
    {
        //Decrease the numbers of buttons, and if we got them all, we win the minigame, and tell the broadcaster
        buttonsLeft--;
        if (buttonsLeft <= 0)
        {
            MinigameBroadcaster.MinigameCompleted();
        }
        Destroy(button);
    }
}
