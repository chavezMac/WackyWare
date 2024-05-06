using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BossMinigameLogic : MonoBehaviour
{
    public int buildingsRemaining = 0;
    public bool debug = false;
    public MinigameMusic music;
    public Camera mainCamera;
    public GameObject helicopter;
    public GameObject godzilla;
    public float helicopterWaveDelay = 10f;
    public Vector3[] spawnPoints;
    public HelicopterTargetIndicator heliWarning;
    private int helicoptersSpawned = 0;
    public Animator instruction;
    public Animator controls;

    public AudioClip victoryMusic;
    public AudioClip failureMusic;
    public AudioSource audioSource;

    private void Start()
    {
        MinigameBroadcaster.SetGameTimerPauseState(true);
        godzilla = GameObject.FindWithTag("Player");
        StartCoroutine(SpawnHelicopterRoutine());
        StartCoroutine(SpawnHelicopterRoutine2());
        instruction.speed = .4f;
        controls.speed = .4f;
        audioSource = GetComponent<AudioSource>();
    }

    public void Lose()
    {
        music.FadeOutMusicFailure();
        StartCoroutine(PlaySoundAndWait(failureMusic, false));
    }

    public void Win()
    {
        music.FadeOutMusic();
        StartCoroutine(PlaySoundAndWait(victoryMusic, true));
    }
    
    IEnumerator PlaySoundAndWait(AudioClip clip, bool win)
    {
        MinigameBroadcaster.SetGameTimerPauseState(true);
        //lower the volume of other sources
        var wod = godzilla.GetComponent<WodzillaController>();
        wod.roar.volume /= 3;
        wod.stomp.volume /= 3;
        wod.miscsfx.volume /= 2;
        wod.lasersfx.volume /= 2;
        wod.impact.volume /= 2;
        // Play the sound
        audioSource.clip = clip;
        audioSource.Play();

        // Wait until the sound finishes playing
        yield return new WaitForSeconds(clip.length);

        if (win)
        {
            MinigameBroadcaster.MinigameCompleted();
        }
        else
        {
            MinigameBroadcaster.MinigameFailed();
        }
    }

    void Update()
    {
        if (MainGameController.timeRemaining <= 0 && !MainGameController.timerPaused)//Lose condition
        {
            Lose();
        }

        if (debug && Input.GetKeyDown(KeyCode.E))//DEBUG WIN CONDITION
        {
            Win();
        }
        
        if (debug && Input.GetKeyDown(KeyCode.F))//DEBUG FAIL CONDITION
        {
            Lose();
        }
    }

    private IEnumerator SpawnHelicopterRoutine()
    {
        while (MainGameController.timerPaused)
        {
            yield return null;
        }
        while (true)
        {
            // Debug.Log("ready to spawn heli");
            yield return new WaitForSeconds(helicopterWaveDelay);

            // Spawn a helicopter
            GameObject heli = SpawnHelicopterNearby();
            helicoptersSpawned++;
            StartHelicopterWarning(heli);

            // Wait until the helicopter is destroyed
            yield return new WaitUntil(() => heli == null);
        }
    }
    
    private IEnumerator SpawnHelicopterRoutine2()
    {
        while (MainGameController.timerPaused)
        {
            yield return null;
        }
        while (true)
        {
            yield return new WaitForSeconds(helicopterWaveDelay*1.2f);

            // Spawn a helicopter
            GameObject heli = SpawnHelicopterAtRandomSpot();
            helicoptersSpawned++;
            StartHelicopterWarning(heli);

            // Wait until the helicopter is destroyed
            yield return new WaitUntil(() => heli == null);
        }
    }

    public void UpdateBuildingCount(int numToAdd)
    {
        buildingsRemaining += numToAdd;
        if (buildingsRemaining <= 0) //Win condition
        {
            Win();
        }
    }

    private GameObject SpawnHelicopterAtRandomSpot()
    {
        // Shuffle the array of spawn points
        ShuffleArray(spawnPoints);
        // Iterate through each spawn point
        foreach (Vector3 spawnPoint in spawnPoints)
        {
            // Check if the spawn point is within the camera's field of view
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(spawnPoint);
            if (!IsPointInView(viewportPoint))
            {
                // Spawn a helicopter at the spawn point
                // Debug.Log("Spawning helicopter at: " + spawnPoint.ToString());
                GameObject heli = Instantiate(helicopter, spawnPoint, Quaternion.identity);
                return heli;
            }
        }
        //if no good point is found, just pick one
        // Debug.Log("No good spawn point found...");
        GameObject defaultSpawnPoint = Instantiate(helicopter, spawnPoints[0], Quaternion.identity);
        return defaultSpawnPoint;
    }
    
    private GameObject SpawnHelicopterNearby()
    {
        Vector3 nearestHeli = default;
        float nearestDistance = Mathf.Infinity;
        bool foundASpot = false;

        // Iterate through each spawn point
        foreach (Vector3 spawnPoint in spawnPoints)
        {
            // Calculate the distance to Godzilla
            float distanceToGodzilla = Vector3.Distance(spawnPoint, godzilla.transform.position);

            // Check if the spawn point is within the camera's field of view
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(spawnPoint);
            if (!IsPointInView(viewportPoint))
            {
                // Check if this spawn point is closer to Godzilla than the previous nearest one
                if (distanceToGodzilla < nearestDistance)
                {
                    nearestDistance = distanceToGodzilla;
                    nearestHeli = spawnPoint;
                    foundASpot = true;
                }
            }
        }

        // If a suitable spawn point is found, spawn a helicopter at that point
        if (foundASpot)
        {
            // Debug.Log("Spawning helicopter at: " + nearestHeli);
            return Instantiate(helicopter, nearestHeli, Quaternion.identity);
        }
        else
        {
            // If no suitable spawn point is found, just pick one
            // Debug.Log("Spawning helicopter at: " + spawnPoints[0]);
            return Instantiate(helicopter, spawnPoints[0], Quaternion.identity);
        }
    }

    public GameObject SpawnHelicopertAtSpot(Vector3 point)
    {
        // Debug.Log("Spawning scripted helicopter at: " + point);
        helicoptersSpawned++;
        
        var heli = Instantiate(helicopter, point, Quaternion.identity);
        StartCoroutine(StartHelicopterWarning(heli));
        // StartHelicopterWarning(heli);
        return heli;
    }

    
    private void ShuffleArray(Vector3[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }
    
    private bool IsPointInView(Vector3 viewportPoint)
    {
        return viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1 && viewportPoint.z > 0;
    }

    private IEnumerator StartHelicopterWarning(GameObject heli)
    {
        while (MainGameController.timerPaused)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        if (helicoptersSpawned == 1)
        {
            heliWarning.helicopterTransform = heli.transform;
            heliWarning.gameObject.SetActive(true);
            heliWarning.SetScreenPosition();
        }
    }
}
