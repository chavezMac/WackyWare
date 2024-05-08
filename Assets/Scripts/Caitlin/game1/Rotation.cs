using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public int duckNum;
    public AudioSource audioSource;

    float tolerance = 0.0001f;

    void Start()
    {
        // Set a random rotation
        int randomRotation = Random.Range(1, 4) * 90;
        transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);

        if ((duckNum == 1 && Mathf.Abs(transform.eulerAngles.z - 90f) < tolerance) ||
                (duckNum == 2 && Mathf.Abs(transform.eulerAngles.z - 270f) < tolerance) ||
                (duckNum == 3 && Mathf.Abs(transform.eulerAngles.z - 180f) < tolerance) ||
                (duckNum == 4 && Mathf.Abs(transform.eulerAngles.z - 0f) < tolerance) ||
                (duckNum == 5 && Mathf.Abs(transform.eulerAngles.z - 180f) < tolerance) ||
                (duckNum == 6 && Mathf.Abs(transform.eulerAngles.z - 0f) < tolerance) ||
                (duckNum == 7 && Mathf.Abs(transform.eulerAngles.z - 270f) < tolerance)) {
                     UprightManager.instance.uprightCount++;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                     Debug.Log("Frozen!");
            }
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            if (audioSource != null && audioSource.clip != null)
                {
                audioSource.Play();
                }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

  
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                // Rotate the cube 
                transform.Rotate(Vector3.forward, rotationSpeed);


            if ((duckNum == 1 && Mathf.Abs(transform.eulerAngles.z - 90f) < tolerance) ||
                (duckNum == 2 && Mathf.Abs(transform.eulerAngles.z - 270f) < tolerance) ||
                (duckNum == 3 && Mathf.Abs(transform.eulerAngles.z - 180f) < tolerance) ||
                (duckNum == 4 && Mathf.Abs(transform.eulerAngles.z - 0f) < tolerance) ||
                (duckNum == 5 && Mathf.Abs(transform.eulerAngles.z - 180f) < tolerance) ||
                (duckNum == 6 && Mathf.Abs(transform.eulerAngles.z - 0f) < tolerance) ||
                (duckNum == 7 && Mathf.Abs(transform.eulerAngles.z - 270f) < tolerance))
                {
                     UprightManager.instance.uprightCount++;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                     Debug.Log("Frozen!");
            }
        }
        

        //Check if all three objects are upright
        if (UprightManager.instance.uprightCount == 7)
        {
            Debug.Log("All objects are upright!");
            
            MinigameBroadcaster.MinigameCompleted(); 
        }
    }

    //Check if time has run out, and if so, we fail the minigame
    if (MainGameController.timeRemaining <= 0 && !MainGameController.timerPaused)
    {
        MinigameBroadcaster.MinigameFailed();
    }
}
}
    


