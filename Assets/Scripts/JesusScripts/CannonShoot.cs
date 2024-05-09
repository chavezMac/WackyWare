using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : MonoBehaviour
{

    public GameObject CannonBall;
    public float shootForce = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectile = (GameObject)Instantiate( CannonBall, transform.position, transform.rotation);

            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward*shootForce);
        }
    }
}
