using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2GameManager : MonoBehaviour
{
    public GameObject PipeHolder;
    private PipeScript[] pipeScripts; // Array to store references to PipeScript components
    private int totalPipes;

    void Start()
    {
        totalPipes = PipeHolder.transform.childCount;
        Debug.Log("total pipes = " + totalPipes);
        pipeScripts = new PipeScript[totalPipes];

        for (int i = 0; i < totalPipes; i++)
        {
            GameObject pipeObject = PipeHolder.transform.GetChild(i).gameObject;
            pipeScripts[i] = pipeObject.GetComponent<PipeScript>(); // Get reference to PipeScript component
        }
    }

    // Method to check correct pipes
    public void CheckCorrectPipes(int currentCorrectPipes)
    {
        if (currentCorrectPipes == totalPipes)
        {
            Debug.Log("All pipes placed correctly!");
            FindObjectOfType<MainGameController>().MinigameDone(true);
        }
    }
}
