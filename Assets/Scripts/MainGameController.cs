using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameController: MonoBehaviour
{
    private string currentMinigame;
    public int currentMinigameIndex = 0;
    public string[] miniGameList; //list of minigames by their scene name
    public static float timeRemaining; //time left for the current minigame 
    public PieTimer timer;
    void Start()
    {
        currentMinigame = miniGameList[0];
        StartNextMinigame();
    }

    private void Update()
    {
        timeRemaining = timer.currentTime;
    }

    private void StartNextMinigame()
    {
        currentMinigameIndex++;//increment the minigame counter
        if (currentMinigameIndex >= miniGameList.Length)
        {
            Debug.Log("You beat all the games in the collection! Congrats!");
            return;
        }
        currentMinigame = miniGameList[currentMinigameIndex];
        // We can load level scenes additively so we have multiple scenes loaded at once.
        // One scene (GameLogicScene) for the UI and outer game logic,
        // and another for the minigame and it's logic.
        SceneManager.LoadScene(currentMinigame, LoadSceneMode.Additive);
        timeRemaining = 10f;//We can change this later
        timer.StartTimer();
    }

    public void LevelCompleted()
    {
        Debug.Log("MainGameController received the broadcast that the level was completed.");
        SceneManager.UnloadSceneAsync("Example Minigame");
        StartNextMinigame();
    }
}