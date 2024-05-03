using UnityEngine;

public class WodzillaTail : MonoBehaviour
{
    public WodzillaController wodzilla;
    public bool isActive = false;
    public int tailDamage = 50;

    void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("WodzillaDestructible"))
        {
            WodzillaBuilding building = other.gameObject.GetComponent<WodzillaBuilding>();
            if (other != null)
            {
                building.TakeDamage((float)tailDamage);
                wodzilla.PlayRandomImpactSound();
            }
        }
    }
}
