using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UprightManager : MonoBehaviour
{
   // Singleton instance
    public static UprightManager instance;

    // Counter to track the number of upright objects
    public int uprightCount = 0;

    private void Awake()
    {
        // Ensure only one instance of UprightManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
