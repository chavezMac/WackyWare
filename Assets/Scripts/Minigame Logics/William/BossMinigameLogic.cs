using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinigameLogic : MonoBehaviour
{
    public int buildingsRemaining = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MainGameController.timeRemaining <= 0)
        {
            MinigameBroadcaster.MinigameFailed();
        }
    }

    public void UpdateBuildingCount(int numToAdd)
    {
        buildingsRemaining += numToAdd;
        if (buildingsRemaining <= 0)
        {
            MinigameBroadcaster.MinigameCompleted();
        }
    }
}
