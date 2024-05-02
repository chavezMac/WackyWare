using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public bool isDisplay;
    public int duckNum;

    // Flag to indicate if the object is upright
    private bool isUpright = false;

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

                //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);

                // Check if the cube is upright after rotation

                if(duckNum == 1 && transform.localEulerAngles.z == 90f){
                    UprightManager.instance.uprightCount++;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    Debug.Log("Frozen!");
                }else if(duckNum == 2 && transform.localEulerAngles.z == -90f){
                    UprightManager.instance.uprightCount++;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                }else if(duckNum == 3 && transform.localEulerAngles.z == 180f){
                    UprightManager.instance.uprightCount++;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                }else if(duckNum == 4 && transform.localEulerAngles.z == 0f){
                    UprightManager.instance.uprightCount++;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                }
                else if(duckNum == 5 && transform.localEulerAngles.z == 180f){
                    UprightManager.instance.uprightCount++;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                }else if(duckNum == 6 && transform.localEulerAngles.z == 0f){
                    UprightManager.instance.uprightCount++;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                }}else if(duckNum == 7 && transform.localEulerAngles.z == -90){
                    UprightManager.instance.uprightCount++;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                }
            
            }
        

        //Check if all three objects are upright
        if (UprightManager.instance.uprightCount == 7)
        {
            Debug.Log("All objects are upright!");
            // Return Successful
            FindObjectOfType<MainGameController>().MinigameDone(true);
        }
    }
}
    


