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
    public static float minigameTimeLimit = 10f;
    public static bool timerPaused = true;
    public bool debug = true;
    private bool timerOverloaded = false;
    public Camera mainSceneCamera;
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
        Debug.Log("mgc start");
        DontDestroyOnLoad(this);
        if(!timerOverloaded)
        {
            timeRemaining = minigameTimeLimit;
        }
        _minigameBroadcaster = FindObjectOfType<MinigameBroadcaster>();
        if (_minigameBroadcaster!=null && _minigameBroadcaster.demoMode)
        {
            onlyMinigame = _minigameBroadcaster.currentScene.name;
            demomode = true;
            currentMinigame = onlyMinigame;
        }
        transition.init(); //Starts clapperboard mid-animation
        currentMinigame = miniGameList[0];
        sfx = GetComponent<AudioSource>();
        WinIcon.speed = 0;
        FailIcon.speed = 0;
        WinIcon.Play(0);
        WinIcon.Play(0);
        StartNextMinigame(true);
    }

    private void Update()
    {
        sfx.volume = 1f;
        float vol = 1f;
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(DebugLoadWodzilla());
        }
        
        if (!timerPaused)
        {
            timeRemaining -= Time.deltaTime;
            // Play clock ticking sound if we're running out of time
            if (timeRemaining < ticktockSound.length && !sfx.isPlaying)
            {
                sfx.clip = ticktockSound;
                sfx.Play();
            }
            //Control volume of clock ticking
            if (sfx.clip == ticktockSound && sfx.isPlaying)
            {
                vol = Mathf.Clamp(.1f + (ticktockSound.length - timeRemaining) / ticktockSound.length,0f,1f);
                sfx.volume = vol;
            }
        }
    }
    
    private IEnumerator DebugLoadWodzilla()
    {
        SceneManager.UnloadSceneAsync(currentMinigame);
        yield return new WaitForSeconds(.2f);
        currentMinigame = "ExampleMinigame1";
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadSceneAsync(currentMinigame);
    }

    public void DemoSingleMinigame(string sceneName)
    {
        //Play the current minigame repeatedly
        for(int i = 0; i < miniGameList.Length;i++)
        {
            miniGameList[i] = sceneName;
            onlyMinigame = sceneName;
            currentMinigame = sceneName;
        }
    }

    public AsyncOperation LoadMinigame()
    {
        // if (SceneManager.GetSceneByName(currentMinigame).isLoaded)
        // {
        //     return null;
        // }
        if (debug)
        {
            Debug.Log("Loading minigame scene: " + currentMinigame);
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentMinigame);
        return asyncLoad;
    }

    private IEnumerator StartNextMinigameCoroutine(bool isFirstMinigame)
    {
        timerPaused = true;
        yield return null; //pause for 1 frame
        
        currentMinigameIndex++; // Increment the minigame counter
        if (currentMinigameIndex >= miniGameList.Length)
        {
            if (demomode)
            {
                currentMinigameIndex = 0;
            }
            else
            {
                Debug.Log("You beat all the games in the collection! Congrats!");
                yield break;
            }
        }
        
        //Play transition animation if there was a previous minigame
        if (!isFirstMinigame)
        {
            transition.Play();
        }
        //Apply random names to the clapper board
        SetClapperNames();
        //Wait for a moment and unload the scene after the animation completes
        
        if (!isFirstMinigame)
        {
            yield return new WaitForSeconds(.45f);
        }

        if (!timerOverloaded)
        {
            timeRemaining = minigameTimeLimit;
            timer.StartTimer(minigameTimeLimit);
        }
        timerOverloaded = false;
        
        yield return new WaitForSeconds(.1f);
        
        if (onlyMinigame != "")
        {
            currentMinigame = onlyMinigame;
        }
        else
        {
            currentMinigame = miniGameList[currentMinigameIndex];
        }
        
        // Pause to show animations.
        if (!isFirstMinigame) // if there was a minigame before this one, pause for a moment.
        {
            yield return new WaitForSeconds(1.35f);
        }
        else //otherwise, much shorter pause
        {
            yield return new WaitForSeconds(.05f);
        }
        //After the pause, load the scene, and continue the animation after loading is done.
        AsyncOperation asyncOperation = LoadMinigame();
        if (asyncOperation != null)
        {
            asyncOperation.completed += (AsyncOperation async) =>
            {
                transition.ResumeAnimation();
            };
        }
        else
        {
            transition.ResumeAnimation();
        }
        timerPaused = false;
    }

    private void SetClapperNames()
    {
        clapper.UpdateClapperText(currentMinigameIndex,
            teamNames[Random.Range(0, 6)].ToUpper(),
            teamNames[Random.Range(0, 6)].ToUpper(),
            teamNames[Random.Range(0, 6)].ToUpper());
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

    public void OverrideTimeLimit(float newTimeLimit)
    {
        if (newTimeLimit <= -1)
        {
            return; //use default time limit
        }
        timerOverloaded = true;
        if (newTimeLimit > 120f)
        {
            newTimeLimit = 120f;
        }

        if (newTimeLimit < 2f)
        {
            newTimeLimit = 2f;
        }
        timeRemaining = newTimeLimit;
        timerPaused = false;
        timer.StartTimer(newTimeLimit);
    }
}