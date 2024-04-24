using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    
    public GameManagerScript GameManagerScript;
    public GameObject[] PlanetsPrefabs;
    public int[] PlanetsOrder;
    public RectTransform spawnRectTransform;
    private int color;
    private int lastPointUsed;
    public SortedDictionary<GameObject ,int> solarSystemMap;
    private int i = 0;
    public RectTransform[] spawnRectTransforms; // Reference to the spawn object's RectTransform
    //private bool isBalloonSpawned = false;
    //private float[] spawnPoints = { -333f, -222f, -111f, 0f, 111f, 222f, 333f };
    

    // Start is called before the first frame update
     void Start()
     {
    //     foreach (var kvp in PlanetsPrefabs)
    //     {
    //         solarSystemMap.Add(PlanetsPrefabs[i], PlanetsOrder[i]);
    //         i++;
    //     }
        
     }

    // Update is called once per frame
    void Update()
    {
        
        int randPositionIndex = Random.Range(0, spawnRectTransforms.Length); 

        color = Random.Range(0, PlanetsPrefabs.Length);
        if(randPositionIndex == lastPointUsed) {
            while (randPositionIndex == lastPointUsed)
            {
                randPositionIndex = Random.Range(0, spawnRectTransforms.Length);
                
            }
            spawnRectTransform  = spawnRectTransforms[randPositionIndex];
            
        }else{
            
            spawnRectTransform = spawnRectTransforms[randPositionIndex];
            lastPointUsed = randPositionIndex;
        }
            Vector3 spawnPosition = spawnRectTransform.position;
            //Vector3 spawnPosition = new Vector3(spawnRectTransform.position.x, spawnRectTransform.position.y, spawnRectTransform.position.z);
            StartCoroutine(InstantiateWithDelay(spawnPosition));
            
        
        
    }

    IEnumerator InstantiateWithDelay(Vector3 spawnPosition)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(2f);
        Instantiate(PlanetsPrefabs[color], spawnPosition, Quaternion.identity, spawnRectTransform);

    }
}