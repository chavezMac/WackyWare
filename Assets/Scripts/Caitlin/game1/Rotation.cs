using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public int duckNum;

    void Start()
    {
        // Set a random rotation for the cube that is a multiple of 90 degrees
        int randomRotation = Random.Range(1, 4) * 90;
        transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);

    }

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits this cube
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                // Rotate the cube by 90 degrees
                transform.Rotate(Vector3.forward, rotationSpeed);

                // Check if the cube is right after rotation

            if ((duckNum == 1 && Mathf.Approximately(transform.localEulerAngles.z, 90f)) ||
                (duckNum == 2 && Mathf.Approximately(transform.localEulerAngles.z, 270)) ||
                (duckNum == 3 && Mathf.Approximately(transform.localEulerAngles.z, 180f)) ||
                (duckNum == 4 && Mathf.Approximately(transform.localEulerAngles.z, 0f)) ||
                (duckNum == 5 && Mathf.Approximately(transform.localEulerAngles.z, 180f)) ||
                (duckNum == 6 && Mathf.Approximately(transform.localEulerAngles.z, 0f)) ||
                (duckNum == 7 && Mathf.Approximately(transform.localEulerAngles.z, 270))) {
                     UprightManager.instance.uprightCount++;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                     Debug.Log("Frozen!");
            }
        }
        

        //Check if all three objects are upright
        if (UprightManager.instance.uprightCount == 7)
        {
            Debug.Log("All objects are upright!");
            // Return Successful
        //    FindObjectOfType<MainGameController>().MinigameDone(true);
            MinigameBroadcaster.MinigameCompleted(); 
        }
    }
}
}
    


