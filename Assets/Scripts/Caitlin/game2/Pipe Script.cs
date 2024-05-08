using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] rotations = { 0,90,180,270 };

    public float[] correctRotation;
    [SerializeField]
    bool isPlaced = false;
    public AudioSource audioSource;

    int PossibleRots = 1;
    float tolerance = 0.0001f;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        PossibleRots = correctRotation.Length;
    int rand = Random.Range(0, rotations.Length);
    transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

    if (PossibleRots > 1)
    {
        if (Mathf.Abs(transform.eulerAngles.z - correctRotation[0]) < tolerance || Mathf.Abs(transform.eulerAngles.z - correctRotation[1]) < tolerance)
        {
            isPlaced = true;
            gameManager.correctMove();
        }
        else
        {
            isPlaced = false;
        }
    }
    else
    {
        if (Mathf.Abs(transform.eulerAngles.z - correctRotation[0]) < tolerance)
        {
            isPlaced = true;
            gameManager.correctMove();
        }
        else
        {
            isPlaced = false;
        }
    }
    }

    private void OnMouseDown()
    {
        if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
        
        transform.Rotate(new Vector3(0, 0, 90));

        if (PossibleRots > 1)
        {
            if (Mathf.Abs(transform.eulerAngles.z - correctRotation[0]) < tolerance || (Mathf.Abs(transform.eulerAngles.z - correctRotation[1]) < tolerance) && isPlaced == false)
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                gameManager.wrongMove();
            }
        }
        else
        {
            Debug.Log(transform.eulerAngles.z);
            Debug.Log(Mathf.Abs(transform.eulerAngles.z - correctRotation[0]) < tolerance);
            if (Mathf.Abs(transform.eulerAngles.z - correctRotation[0]) < tolerance && !isPlaced)
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                gameManager.wrongMove();
            }
        }
    }
}
