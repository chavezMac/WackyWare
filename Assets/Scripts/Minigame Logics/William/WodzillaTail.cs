using UnityEngine;

public class WodzillaTail : MonoBehaviour
{
    public WodzillaController wodzilla;
    public bool isActive = false;

    private void Start()
    {
        // Debug.Log("Tail start");
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("WodzillaDestructible"))
        {
            WodzillaBuilding building = other.gameObject.GetComponent<WodzillaBuilding>();
            if (other != null)
            {
                building.TakeDamage(50);
                wodzilla.PlayRandomImpactSound();
            }
        }
    }
}
