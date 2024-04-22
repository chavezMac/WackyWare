using System.Collections;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameManagerScript GameManagerScript;
    public GameObject[] balloonPrefabs;
    public RectTransform spawnRectTransform;
    private int color;
    private int lastPointUsed;
    
    public RectTransform[] spawnRectTransforms; // Reference to the spawn object's RectTransform
    private bool isBalloonSpawned = false;
    //private float[] spawnPoints = { -333f, -222f, -111f, 0f, 111f, 222f, 333f };
    

    // Start is called before the first frame update
    private void Start()
    {
        lastPointUsed = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
        int randPositionIndex = Random.Range(0, spawnRectTransforms.Length); 

        color = Random.Range(0, balloonPrefabs.Length);
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
        if (!isBalloonSpawned && GameManagerScript.balloonsPopped > 0)
        {
            Vector3 spawnPosition = spawnRectTransform.position;
            //Vector3 spawnPosition = new Vector3(spawnRectTransform.position.x, spawnRectTransform.position.y, spawnRectTransform.position.z);
            StartCoroutine(InstantiateWithDelay(spawnPosition));
            
            isBalloonSpawned = true;
        }
    }

    IEnumerator InstantiateWithDelay(Vector3 spawnPosition)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(2f);
        isBalloonSpawned = false;
        Instantiate(balloonPrefabs[color], spawnPosition, Quaternion.identity, spawnRectTransform);

    }
}
