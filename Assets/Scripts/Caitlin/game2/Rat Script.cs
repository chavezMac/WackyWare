using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatScript : MonoBehaviour
{
    public GameObject ratPrefab;
    public int numberOfRats;

    void Start()
    {
        for (int i = 0; i < numberOfRats; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            Instantiate(ratPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
