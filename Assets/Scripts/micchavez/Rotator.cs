using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour 
{
    // Update is called once per frame
    void Update(){
        //Rotate the object around its local Y axis
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);

        //transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        
    }
}
