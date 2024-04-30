using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameBroadcaster : MonoBehaviour
{
    [HideInInspector] 
    public GameObject mainGameControllerPrefab;
    private static MainGameController gameController;
    [HideInInspector] public Animator inputAmin;
    private static bool gameFinished = false;
    private static bool hookedIn = false;
    [HideInInspector] public Scene currentScene;
    [HideInInspector] public bool demoMode = true;
    [HideInInspector] public GameObject mouseIcon;
    [HideInInspector] public GameObject WASDIcon;
    [HideInInspector] public GameObject SpaceIcon;
    public bool isMouseUsed = false;
    public bool isWASDUsed = false;
    public bool isSpacebarUsed = false;
    

    // Edit this if you want your minigame to start with more or less time in seconds.
    [Header("Specify time limit below, or leave at -1 for default time limit")]
    [Tooltip("Set to how long the minigame should last, or -1 to leave it at default time limit")]
    public float overrideTimeLimit = -1f;
    void Start()
    {
        gameController = FindObjectOfType<MainGameController>();
        gameFinished = false;
        //Minigame is being played in sequence by the MainGameController
        if (gameController != null)
        {
            gameController.SetTimeLimit(overrideTimeLimit);
            hookedIn = true;
            demoMode = false;
        }
        //If the minigame is being loaded up on it's own, we just demo the game repeatedly
        else
        {
            Debug.Log("MainGameController not present, starting demo mode");
            gameController = Instantiate(mainGameControllerPrefab).GetComponent<MainGameController>();
            currentScene = SceneManager.GetActiveScene();
            gameController.DemoSingleMinigame(currentScene.name);
            gameController.SetTimeLimit(overrideTimeLimit);
            hookedIn = true;
        }
        mouseIcon.SetActive(isMouseUsed);
        WASDIcon.SetActive(isWASDUsed);
        SpaceIcon.SetActive(isSpacebarUsed);
        inputAmin.Play(0);
    }
    
    public static void MinigameCompleted()
    {
        if (!gameFinished)
        {
            Debug.Log("The minigame reports: Level completed!");
            if (hookedIn)
            {
                gameController.MinigameDone(true);
            }
            gameFinished = true;
        }
    }

    public static void MinigameFailed()
    {
        if (!gameFinished)
        {
            Debug.Log("The minigame reports: Level failed");
            if (hookedIn)
            {
                gameController.MinigameDone(false);
            }
            gameFinished = true;
            
        }
    }
}