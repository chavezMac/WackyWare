using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2GameManager : MonoBehaviour
{
    public GameObject PipeHolder;
    public GameObject[] Pipes;

    [SerializeField]
    public int totalPipes = 0;
    // Start is called before the first frame update
    void Start()
    {
        totalPipes = PipeHolder.transform.childCount;
        Pipes = new GameObject[totalPipes];
        for(int i = 0; i < Pipes.Length; i++){
            Pipes[i] = PipeHolder.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
