using UnityEngine;

public class BossMinigameLogic : MonoBehaviour
{
    public int buildingsRemaining = 0;
    public bool debug = true;
    public MinigameMusic music;

    // Update is called once per frame
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

    public void UpdateBuildingCount(int numToAdd)
    {
        buildingsRemaining += numToAdd;
        if (buildingsRemaining <= 0) //Win condition
        {
            MinigameBroadcaster.MinigameCompleted();
            music.FadeOutMusic();
        }
    }
}
