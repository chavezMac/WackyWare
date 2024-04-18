using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{
    private Quaternion targetRotation;
    private bool isSelected = false;
    private Vector3 lastMousePosition;

    void Start()
    {
        // Generate a random rotation around the Z-axis
        float randomZRotation = Random.Range(0f, 360f);
        targetRotation = Quaternion.Euler(0, 0, randomZRotation);
        transform.rotation = targetRotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isSelected = true;
                    lastMousePosition = Input.mousePosition;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
        }

        // Check if the user has interacted with this object
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Rotate the object
            transform.Rotate(Vector3.back, mouseX * 5f, Space.World);
            transform.Rotate(Vector3.right, mouseY * 5f, Space.World);
        }

        // Check if the object is in correct position
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            Debug.Log("Object is in correct position!");
        }
    }
}
