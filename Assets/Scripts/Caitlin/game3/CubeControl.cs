using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeControl : MonoBehaviour
{
    [SerializeField] KeyCode keyFirst, keySecond;
    // private KeyCode keyRestart = KeyCode.R, keyNext = KeyCode.Q;
    [SerializeField] Vector3 cubeMove;
    
    private void FixedUpdate()
    {
        if (Input.GetKey(keyFirst)){
            GetComponent<Rigidbody>().velocity += cubeMove;
        }
        if (Input.GetKey(keySecond)){
            GetComponent<Rigidbody>().velocity -= cubeMove;
        }
        // if (Input.GetKey(keyRestart)){
        //     SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        // }
        
    }


}
