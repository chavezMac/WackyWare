using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject[] Pipes;

    [SerializeField]
    int totalPipes = 0;
    [SerializeField]
    public int correctedPipes = 0;


    // Start is called before the first frame update
    void Start()
    {
        totalPipes = PipesHolder.transform.childCount;

        Debug.Log("total pipes:" + totalPipes);

        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void correctMove()
    {
        correctedPipes += 1;

        Debug.Log("total: "+  correctedPipes);

        if(correctedPipes == totalPipes)
        {
            Debug.Log("You win!");
            MinigameBroadcaster.MinigameCompleted(); 
        }
    }

    public void wrongMove()
    {
        correctedPipes -= 1;
    }

    void Update(){
        //Check if time has run out, and if so, we fail the minigame
        if (MainGameController.timeRemaining <= 0 && !MainGameController.timerPaused)
        {
            MinigameBroadcaster.MinigameFailed();
        }
    }
}
