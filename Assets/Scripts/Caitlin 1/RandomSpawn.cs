using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int numberOfSpawns = 5;
    public float spawnRadius = 5f;

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfSpawns; i++)
        {
            Vector2 randomPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
        }
    }
}
