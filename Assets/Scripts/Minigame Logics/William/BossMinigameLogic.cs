using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossMinigameLogic : MonoBehaviour
{
    public int buildingsRemaining = 0;
    public bool debug = true;
    public MinigameMusic music;
    public Camera mainCamera;
    public GameObject helicopter;
    public GameObject godzilla;
    public float helicopterWaveDelay = 10f;
    public Vector3[] spawnPoints;

    private void Start()
    {
        godzilla = GameObject.FindWithTag("Player");
        StartCoroutine(SpawnHelicopterRoutine());
        StartCoroutine(SpawnHelicopterRoutine2());
    }

    void Update()
    {
        if (MainGameController.timeRemaining <= 0 && !MainGameController.timerPaused)//Lose condition
        {
            MinigameBroadcaster.MinigameFailed();
            music.FadeOutMusicFailure();
        }

        if (debug && Input.GetKeyDown(KeyCode.E))//DEBUG WIN CONDITION
        {
            MinigameBroadcaster.MinigameCompleted();
            music.FadeOutMusic();
        }
        
        if (debug && Input.GetKeyDown(KeyCode.F))//DEBUG FAIL CONDITION
        {
            MinigameBroadcaster.MinigameFailed();
            music.FadeOutMusicFailure();
        }
    }

    private IEnumerator SpawnHelicopterRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(helicopterWaveDelay);

            // Spawn a helicopter
            GameObject helicopter = SpawnHelicopterNearby();

            // Wait until the helicopter is destroyed
            yield return new WaitUntil(() => helicopter == null);
        }
    }
    
    private IEnumerator SpawnHelicopterRoutine2()
    {
        while (true)
        {
            yield return new WaitForSeconds(helicopterWaveDelay+3f);

            // Spawn a helicopter
            GameObject helicopter = SpawnHelicopterAtRandomSpot();

            // Wait until the helicopter is destroyed
            yield return new WaitUntil(() => helicopter == null);
        }
    }

    public void UpdateBuildingCount(int numToAdd)
    {
        buildingsRemaining += numToAdd;
        if (buildingsRemaining <= 0) //Win condition
        {
            MinigameBroadcaster.MinigameCompleted();
            music.FadeOutMusic();
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
                Debug.Log("Spawning helicopter at: " + spawnPoint.ToString());
                GameObject heli = Instantiate(helicopter, spawnPoint, Quaternion.identity);
                return heli;
            }
        }
        //if no good point is found, just pick one
        Debug.Log("No good spawn point found...");
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
            return Instantiate(helicopter, nearestHeli, Quaternion.identity);
        }
        else
        {
            // If no suitable spawn point is found, just pick one
            return Instantiate(helicopter, spawnPoints[0], Quaternion.identity);
        }
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
}
