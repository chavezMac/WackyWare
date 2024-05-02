using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] GameObject[] Hurdles;
    [SerializeField] Material MaterialsHide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<Renderer>().material = MaterialsHide;

            foreach (var hurdle in Hurdles)
            {
                hurdle.GetComponent<Renderer>().material = MaterialsHide;
                hurdle.GetComponent<Collider>().isTrigger = true;
            }
        }
    }
}
