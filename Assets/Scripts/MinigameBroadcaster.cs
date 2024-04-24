using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameBroadcaster : MonoBehaviour
{
    private static MainGameController gameController;
    private static bool gameFinished = false;
    private static bool hookedIn = false;
    [HideInInspector] public Scene currentScene;
    [HideInInspector] public bool demoMode = true;
    // public float overrideTimeLimit = -1f;
    void Start()
    {
        // Debug.Log("The scene name is " + sceneName);
        gameController = FindObjectOfType<MainGameController>();
        gameFinished = false;
        //Minigame is being played in sequence by the MainGameController
        if (gameController != null)
        {
            // Debug.Log("game controller found: "+ gameController.ToString());
            hookedIn = true;
            demoMode = false;
        }
        //If the minigame is being loaded up on it's own, we just demo the game repeatedly
        else if (!SceneManager.GetSceneByName("GameLogicScene").isLoaded)
        {
            Debug.Log("MainGameController not present, starting demo mode");
            currentScene = SceneManager.GetActiveScene();
            disableAll();
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameLogicScene",LoadSceneMode.Additive);
            asyncLoad.completed += (AsyncOperation async) =>
            {
                // This code block will be executed when the scene is fully loaded
                gameController = FindObjectOfType<MainGameController>();
                gameController.DemoSingleMinigame(currentScene.name);
                // gameController.tempMinigameTimeLimit = overrideTimeLimit;
                hookedIn = true;
                SceneManager.UnloadSceneAsync(currentScene);
            };
        }
    }

    private void disableAll()
    {
        // Get all GameObjects in the scene
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        // Deactivate all other objects except the current one
        foreach (GameObject obj in allObjects)
        {
            // Check if the object is not the current object
            if (obj != gameObject)
            {
                // Deactivate the object
                obj.SetActive(false);
            }
        }
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