using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject PlanetToKill;
    public GameObject[] PlanetsPrefabs;
    public RectTransform[] spawns;
    public Dictionary<int, GameObject> planets = new Dictionary<int, GameObject>();
    public SortedDictionary<int, string> planetNames = new SortedDictionary<int, string>();
    
    [NonSerialized]
    public int[] planetNumbers = new int [4];
    private ArrayList spawnedIndices = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        InitializePlanetDictionary();

        int planetsToSpawn =  spawns.Length;
        
        for (int i = 0; i < planetsToSpawn; i++)
        {
            int randomIndex = GetRandomIndex();
            spawnedIndices.Add(randomIndex);
            PlanetsPrefabs[randomIndex].transform.Find("Sphere").gameObject.SetActive(false);
            Instantiate(PlanetsPrefabs[randomIndex], spawns[i].position, Quaternion.identity);
            planetNames.Add(randomIndex , PlanetsPrefabs[randomIndex].name);
           



        }

        int j = 0;
        for (int i = 0; i < PlanetsPrefabs.Length; i++)
        {
            if (planetNames.ContainsKey(i))
            {
                planetNumbers[j] = i;
                j++;
            }
        }
    }



    private void InitializePlanetDictionary()
    {
        for (int i = 0; i < PlanetsPrefabs.Length; i++)
        {
            planets.Add(i, PlanetsPrefabs[i]);
        }
    }

    private int GetRandomIndex()
    {
        int randomIndex = Random.Range(0, PlanetsPrefabs.Length);
        while (spawnedIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, PlanetsPrefabs.Length);
        }
        return randomIndex;
    }
}