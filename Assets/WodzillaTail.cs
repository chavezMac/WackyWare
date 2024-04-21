using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WodzillaTail : MonoBehaviour
{
    public bool isActive = true; 

    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.gameObject.tag == "WodzillaDestructible")
        {
            WodzillaBuilding building = other.gameObject.GetComponent<WodzillaBuilding>();
            building.TakeDamage(50);
            Debug.Log("Building hit!");
        }
    }
}
