using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameController: MonoBehaviour
{
    private string currentMinigame;
    public int currentMinigameIndex = 0;
    public string[] miniGameList; //list of minigames by their scene name
    public static float timeRemaining; //time left for the current minigame 
    public PieTimer timer;
    public DoorAnimationController door;
    public int minigamesCompletedSuccessfully = 0;
    public int minigamesFailed = 0;
    
    void Start()
    {
        currentMinigame = miniGameList[0];
        StartNextMinigame(true);
    }

    private void Update()
    {
        timeRemaining = timer.currentTime;
    }

    private IEnumerator StartNextMinigameCoroutine(bool isFirstMinigame)
    {
        if (currentMinigameIndex >= miniGameList.Length)
        {
            Debug.Log("You beat all the games in the collection! Congrats!");
            yield break;
        }
        door.ResumeAnimation();
        yield return new WaitForSeconds(.25f);
        if (!isFirstMinigame)
        {
            SceneManager.UnloadSceneAsync(currentMinigame);
        }
        currentMinigame = miniGameList[currentMinigameIndex];
        
        // Pause to show animations.
        if (!isFirstMinigame)
        {
            yield return new WaitForSeconds(1.75f);
        }
        door.Play();
        // We can load level scenes additively so we have multiple scenes loaded at once.
        // One scene (GameLogicScene) for the UI and outer game logic,
        // and another for the minigame and its logic.
        SceneManager.LoadScene(currentMinigame, LoadSceneMode.Additive);
        
        timeRemaining = 10f; // We can change this later
        timer.StartTimer();
        currentMinigameIndex++; // Increment the minigame counter
        
    }

    public void StartNextMinigame(bool isFirstMinigame)
    {
        StartCoroutine(StartNextMinigameCoroutine(isFirstMinigame));
    }

    public void MinigameDone(bool win)
    {
        Debug.Log("MainGameController received the broadcast that the level " + currentMinigame + " was completed.");
        if (win)
        {
            minigamesCompletedSuccessfully++;
        }
        else
        {
            minigamesFailed++;
        }
        StartNextMinigame(false);
    }
}