using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishingBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public bool atTop;
    public float targetTime = 4.0f;
    public float savedTargetTime;

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public GameObject p5;
    public GameObject p6;
    public GameObject p7;
    public GameObject p8;

    public bool onFish;
    public playerScript playerS;
    public GameObject bobber;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(onFish == true)
        {
            targetTime += Time.deltaTime;
        }
        if (onFish == false)
        {
            targetTime -= Time.deltaTime;
        }

        if(targetTime <= 0.0f || MainGameController.timeRemaining <= 0)
        {
            transform.localPosition = new Vector3(-0.03204f, -0.128f, 0);
            onFish = false;
            playerS.fishGameLossed();
            Destroy(GameObject.Find("bobber(Clone)"));
            targetTime = 4.0f;
        }
        if (targetTime >= 8.0f)
        {
            transform.localPosition = new Vector3(-0.07522f, -0.203f, 0);
            onFish = false;
            playerS.fishGameWon();
            Destroy(GameObject.Find("bobber(Clone)"));
            targetTime = 4.0f;
        }

        if(targetTime >= 0.0f)
        {
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(false);
            p4.SetActive(false);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        if (targetTime >= 1.0f)
        {
            p1.SetActive(true);
            p2.SetActive(false);
            p3.SetActive(false);
            p4.SetActive(false);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        if (targetTime >= 2.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(false);
            p4.SetActive(false);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        if (targetTime >= 3.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(false);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        if (targetTime >= 4.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(false);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        if (targetTime >= 5.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(true);
            p6.SetActive(false);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        if (targetTime >= 6.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(true);
            p6.SetActive(true);
            p7.SetActive(false);
            p8.SetActive(false);
        }
        if (targetTime >= 7.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(true);
            p6.SetActive(true);
            p7.SetActive(true);
            p8.SetActive(false);
        }
        if (targetTime >= 8.0f)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
            p5.SetActive(true);
            p6.SetActive(true);
            p7.SetActive(true);
            p8.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up*5, ForceMode.Impulse);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("fish"))
        {
            onFish = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("fish"))
        {
            onFish = false;
        }
    }
}