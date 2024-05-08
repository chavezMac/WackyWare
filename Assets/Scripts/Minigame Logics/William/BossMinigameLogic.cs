using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinigameLogic : MonoBehaviour
{
    public int buildingsRemaining = 0;
    public bool debug = true;
    public MinigameMusic music;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MainGameController.timeRemaining <= 0 && !MainGameController.timerPaused)
        {
            MinigameBroadcaster.MinigameFailed();
            music.FadeOutMusic();
        }

        if (debug && Input.GetKeyDown(KeyCode.E))//DEBUG WIN CONDITION
        {
            MinigameBroadcaster.MinigameCompleted();
            music.FadeOutMusic();
        }
    }

    public void UpdateBuildingCount(int numToAdd)
    {
        buildingsRemaining += numToAdd;
        if (buildingsRemaining <= 0)
        {
            MinigameBroadcaster.MinigameCompleted();
            music.FadeOutMusic();
        }
    }
}
