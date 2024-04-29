using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class WodzillaBuildingDust : MonoBehaviour
{
    public GameObject building; // Reference to the building GameObject

    private ParticleSystem partSystem;
    private Vector3 buildingCenter;
    private Vector3 buildingSize;

    void Start()
    {
        partSystem = GetComponent<ParticleSystem>();

        // Calculate the center and size of the building's BoxCollider in world space
        CalculateBuildingBounds();
        
        // Set the initial size and position of the particle system
        UpdateParticleSystem();
        var vector3 = transform.position;
        vector3.y = 1;
        transform.position = vector3;
    }

    void Update()
    {
        // Update the size and position of the particle system if needed
        //UpdateParticleSystem();
    }

    void CalculateBuildingBounds()
    {
        // Get the building's BoxCollider
        BoxCollider buildingCollider = building.GetComponent<BoxCollider>();

        // Calculate the center and size of the BoxCollider in world space
        buildingCenter = buildingCollider.bounds.center;
        buildingSize = buildingCollider.bounds.size/55f;
    }

    void UpdateParticleSystem()
    {
        // Set the size of the particle system to match the building's size
        partSystem.transform.localScale = buildingSize;

        // Set the position of the particle system to match the building's center
        partSystem.transform.position = buildingCenter;
    }
}