using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameController: MonoBehaviour
{
    public string currentMinigame;
    public string onlyMinigame;// Used if we are just demoing one minigame over and over
    public int currentMinigameIndex = -1;
    public string[] miniGameList; //list of minigames by their scene name
    public static float timeRemaining; //time left for the current minigame 
    public float minigameTimeLimit = 10f;
    // [HideInInspector]
    // public float tempMinigameTimeLimit;
    public bool timerPaused = true;
    public PieTimer timer;
    public TransitionAnimationController transition;
    private MinigameBroadcaster _minigameBroadcaster;
    private bool demomode = false;
    public int minigamesCompletedSuccessfully = 0;
    public int minigamesFailed = 0;

    private AudioSource sfx;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip ticktockSound;

    public Animator WinIcon;
    public Animator FailIcon;
    public ClapperBoard clapper;
    public string[] teamNames;
    void Start()
    {
        _minigameBroadcaster = FindObjectOfType<MinigameBroadcaster>();
        if (_minigameBroadcaster!=null && _minigameBroadcaster.demoMode)
        {
            onlyMinigame = _minigameBroadcaster.currentScene.name;
            demomode = true;
        }
        transition.init();
        currentMinigame = miniGameList[0];
        sfx = GetComponent<AudioSource>();
        StartNextMinigame(true);
        WinIcon.speed = 0;
        FailIcon.speed = 0;
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

    public void DemoSingleMinigame(string sceneName)
    {
        //Play the current minigame repeatedly
        for(int i = 0; i < miniGameList.Length;i++)
        {
            miniGameList[i] = sceneName;
            onlyMinigame = sceneName;
        }
        // StartNextMinigame(true);
    }

    public void UnloadMinigame()
    {
        // Debug.Log("Unloading minigame");
        SceneManager.UnloadSceneAsync(currentMinigame);
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
        
        //Play transition animation
        if (!isFirstMinigame)
        {
            transition.Play();
        }
        else
        {
            // transition.ResumeAnimation();
        }
        //Apply random names to the clapper board
        clapper.UpdateClapperText(currentMinigameIndex,
            teamNames[Random.Range(0, 6)].ToUpper(),
            teamNames[Random.Range(0, 6)].ToUpper(),
            teamNames[Random.Range(0, 6)].ToUpper());
        //Wait for a moment and unload the scene after the animation completes
        yield return new WaitForSeconds(.45f);
        if (!isFirstMinigame)
        {
            UnloadMinigame();
        }
        currentMinigame = miniGameList[currentMinigameIndex];
        if (onlyMinigame != "")
        {
            currentMinigame = onlyMinigame;
        }
        
        // Pause to show animations.
        if (!isFirstMinigame)
        {
            yield return new WaitForSeconds(1.55f);
        }
        transition.ResumeAnimation();
        // We can load level scenes additively so we have multiple scenes loaded at once.
        // One scene (GameLogicScene) for the UI and outer game logic,
        // and another for the minigame and its logic.
        SceneManager.LoadScene(currentMinigame, LoadSceneMode.Additive);
        // Debug.Log("Loading scene: " + currentMinigame);

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
            WinIcon.Play(0);
            WinIcon.speed = 1;
        }
        else
        {
            minigamesFailed++;
            sfx.clip = loseSound;
            sfx.Play();
            FailIcon.Play(0);
            FailIcon.speed = 1;
        }
        StartNextMinigame(false);
    }
}