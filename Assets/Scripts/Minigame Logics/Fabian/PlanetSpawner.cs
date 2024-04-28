using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    
    public GameObject fireballs;
    public GameObject[] PlanetsPrefabs;
   
    public RectTransform[] spawns;
    //public int[] PlanetsOrder;
    private int color;
    private int lastPointUsed;
   
    //private int i = 0; 
    // Reference to the spawn object's RectTransform
    //private bool isBalloonSpawned = false;
    //private float[] spawnPoints = { -333f, -222f, -111f, 0f, 111f, 222f, 333f };
    

    // Start is called before the first frame update
     void Start()
     {
         fireballs.SetActive(true);
          int i = Random.Range(7, 9);
          int j = Random.Range(2, 3);
          int k = Random.Range(0, 1);
          int l = Random.Range(5, 6);
          Instantiate(PlanetsPrefabs[i],spawns[0].position,Quaternion.identity);
          Instantiate(PlanetsPrefabs[j],spawns[2].position,Quaternion.identity);
          Instantiate(PlanetsPrefabs[k],spawns[1].position,Quaternion.identity);
          Instantiate(PlanetsPrefabs[l],spawns[3].position,Quaternion.identity);
          Debug.Log(PlanetsPrefabs[i].name + PlanetsPrefabs[j].name + PlanetsPrefabs[k].name + PlanetsPrefabs[l].name );

         //     foreach (var kvp in PlanetsPrefabs)
         //     {
         //         solarSystemMap.Add(PlanetsPrefabs[i], PlanetsOrder[i]);
         //         i++;
         //     }

     }

    // Update is called once per frame
    void Update()
    {
        

    }


}