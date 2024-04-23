using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{
   // Speed at which the cube rotates
    public float rotationSpeed = 90f;

    // Flag to indicate if the object is upright
    private bool isUpright = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set a random rotation for the cube that is a multiple of 90 degrees
        int randomRotation = Random.Range(1, 4) * 90;
        transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);

        // Check if the cube is initially upright
        if (transform.localEulerAngles.z == 0f)
        {
            UprightManager.instance.uprightCount++;
            isUpright = true;
            // Freeze the rotation if the cube is initially upright
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && !isUpright)
        {
            // Cast a ray from the mouse position into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits this cube
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                // Rotate the cube by 90 degrees around its local z-axis
                transform.Rotate(Vector3.forward, rotationSpeed);

                // Set the z-angle to exactly 0 to ensure accuracy
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);

                // Check if the cube is upright after rotation
                if (transform.localEulerAngles.z == 0f)
                {
                    // Increment the upright count
                    UprightManager.instance.uprightCount++;
                    isUpright = true;
                    // Freeze the rotation if the cube is upright
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                }
                else
                {
                    // Unfreeze the rotation if the cube is not upright
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
            }
        }

        // Check if all three objects are upright
        if (UprightManager.instance.uprightCount == 3)
        {
            Debug.Log("All objects are upright!");
            // You can perform any action here when all objects are upright
            FindObjectOfType<MainGameController>().MinigameDone(true);
        }
    }

}
