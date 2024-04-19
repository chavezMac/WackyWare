using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameController: MonoBehaviour
{
    private string currentMinigame;
    public int currentMinigameIndex = -1;
    public string[] miniGameList; //list of minigames by their scene name
    public static float timeRemaining; //time left for the current minigame 
    public float minigameTimeLimit = 10f;
    public bool timerPaused = true;
    public PieTimer timer;
    public DoorAnimationController door;
    public int minigamesCompletedSuccessfully = 0;
    public int minigamesFailed = 0;

    private AudioSource sfx;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip ticktockSound;

    public GameObject WinIcon;
    public GameObject FailIcon;
    void Start()
    {
        door.PauseAnimationAtMiddle();
        currentMinigame = miniGameList[0];
        sfx = GetComponent<AudioSource>();
        StartNextMinigame(true);
    }

    private void Update()
    {
        if (!timerPaused)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < ticktockSound.length && !sfx.isPlaying)
            {
                sfx.clip = ticktockSound;
                sfx.Play();
            }
        }
    }

    private IEnumerator StartNextMinigameCoroutine(bool isFirstMinigame)
    {
        timerPaused = true;
        currentMinigameIndex++; // Increment the minigame counter
        if (currentMinigameIndex >= miniGameList.Length)
        {
            Debug.Log("You beat all the games in the collection! Congrats!");
            yield break;
        }
        
        //Play door closing animation
        if (!isFirstMinigame)
        {
            door.ResumeAnimation();
        }
        //Wait for a moment and unload the scene after the doors close
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
        WinIcon.SetActive(false);
        FailIcon.SetActive(false);
        // We can load level scenes additively so we have multiple scenes loaded at once.
        // One scene (GameLogicScene) for the UI and outer game logic,
        // and another for the minigame and its logic.
        SceneManager.LoadScene(currentMinigame, LoadSceneMode.Additive);

        timeRemaining = minigameTimeLimit;
        timerPaused = false;
        timer.StartTimer();
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
            sfx.clip = winSound;
            sfx.Play();
            WinIcon.SetActive(true);
        }
        else
        {
            minigamesFailed++;
            sfx.clip = loseSound;
            sfx.Play();
            FailIcon.SetActive(true);
        }
        StartNextMinigame(false);
    }
}