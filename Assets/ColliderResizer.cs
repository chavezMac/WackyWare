using UnityEngine;

public class ColliderResizer : MonoBehaviour
{
    void Start()
    {
        // Get all the building parent objects in the scene
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("WodzillaDestructible");

        // Iterate through each building
        foreach (GameObject building in buildings)
        {
            // Get the mesh renderer of the building model
            Renderer buildingRenderer = building.GetComponentInChildren<Renderer>();

            // Calculate the size of the bounding box of the building model
            Vector3 buildingSize = buildingRenderer.bounds.size;

            // Get or add a BoxCollider component to the building
            BoxCollider collider = building.GetComponent<BoxCollider>();
            if (collider == null)
            {
                collider = building.AddComponent<BoxCollider>();
            }

            // Set the size of the BoxCollider to match the size of the bounding box
            collider.size = buildingSize;
        }
    }
}

