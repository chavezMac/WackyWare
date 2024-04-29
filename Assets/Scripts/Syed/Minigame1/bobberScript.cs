using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobberScript : MonoBehaviour
{

    public bool gameIsOver;
    public Animator bobberAnim;
    public float bobberTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bobberTime += Time.deltaTime;
        if(bobberTime >= 3)
        {
            bobberAnim.Play("bobberFish");
        }
        if(Input.GetKeyDown(KeyCode.P) && bobberTime <= 3)
        {
            Destroy(gameObject);
        }
        if(gameIsOver == true)
        {
            Destroy(gameObject);
        }
    }

    public void gameOver()
    {
        gameIsOver = true;
    }
}