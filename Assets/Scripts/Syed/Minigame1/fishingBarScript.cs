using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fishingBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public bool atTop;
    public float targetTime = 4.0f;
    public float savedTargetTime;

    public Image progressBarGreen;

    public AudioClip reel;
    private AudioSource _source;
    public bool onFish;
    public playerScript playerS;
    public GameObject bobber;
    void Start()
    {
        _source = GetComponent<AudioSource>();
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

        _source.volume = Mathf.Lerp(0.2f,1,targetTime/8f);
        
        float fillAmount = Mathf.Clamp(targetTime / 8f,0f,1f);
        progressBarGreen.fillAmount = fillAmount;

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up*3, ForceMode.Impulse);
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
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class fishingBarScript : MonoBehaviour
// {
//     // Start is called before the first frame update
//     public Rigidbody rb;
//     public bool atTop;
//     public float targetTime = 4.0f;
//     public float savedTargetTime;
//
//     public GameObject p1;
//     public GameObject p2;
//     public GameObject p3;
//     public GameObject p4;
//     public GameObject p5;
//     public GameObject p6;
//     public GameObject p7;
//     public GameObject p8;
//
//     public AudioClip reel;
//     private AudioSource _source;
//     public bool onFish;
//     public playerScript playerS;
//     public GameObject bobber;
//     void Start()
//     {
//         _source = GetComponent<AudioSource>();
//     }
//
//     // Update is called once per frame
//     void FixedUpdate()
//     {
//         if(onFish == true)
//         {
//             targetTime += Time.deltaTime;
//         }
//         if (onFish == false)
//         {
//             targetTime -= Time.deltaTime;
//         }
//
//         if(targetTime <= 0.0f || MainGameController.timeRemaining <= 0)
//         {
//             transform.localPosition = new Vector3(-0.03204f, -0.128f, 0);
//             onFish = false;
//             playerS.fishGameLossed();
//             Destroy(GameObject.Find("bobber(Clone)"));
//             targetTime = 4.0f;
//         }
//         if (targetTime >= 8.0f)
//         {
//             transform.localPosition = new Vector3(-0.07522f, -0.203f, 0);
//             onFish = false;
//             playerS.fishGameWon();
//             Destroy(GameObject.Find("bobber(Clone)"));
//             targetTime = 4.0f;
//         }
//
//         if(targetTime >= 0.0f)
//         {
//             p1.SetActive(false);
//             p2.SetActive(false);
//             p3.SetActive(false);
//             p4.SetActive(false);
//             p5.SetActive(false);
//             p6.SetActive(false);
//             p7.SetActive(false);
//             p8.SetActive(false);
//             _source.volume = 0.2f;
//
//         }
//         if (targetTime >= 1.0f)
//         {
//             p1.SetActive(true);
//             p2.SetActive(false);
//             p3.SetActive(false);
//             p4.SetActive(false);
//             p5.SetActive(false);
//             p6.SetActive(false);
//             p7.SetActive(false);
//             p8.SetActive(false);
//             _source.volume = 0.2f;
//         }
//         if (targetTime >= 2.0f)
//         {
//             p1.SetActive(true);
//             p2.SetActive(true);
//             p3.SetActive(false);
//             p4.SetActive(false);
//             p5.SetActive(false);
//             p6.SetActive(false);
//             p7.SetActive(false);
//             p8.SetActive(false);
//             _source.volume = 0.4f;
//         }
//         if (targetTime >= 3.0f)
//         {
//             p1.SetActive(true);
//             p2.SetActive(true);
//             p3.SetActive(true);
//             p4.SetActive(false);
//             p5.SetActive(false);
//             p6.SetActive(false);
//             p7.SetActive(false);
//             p8.SetActive(false);
//             _source.volume = 0.4f;
//         }
//         if (targetTime >= 4.0f)
//         {
//             p1.SetActive(true);
//             p2.SetActive(true);
//             p3.SetActive(true);
//             p4.SetActive(true);
//             p5.SetActive(false);
//             p6.SetActive(false);
//             p7.SetActive(false);
//             p8.SetActive(false);
//             _source.volume = 0.6f;
//         }
//         if (targetTime >= 5.0f)
//         {
//             p1.SetActive(true);
//             p2.SetActive(true);
//             p3.SetActive(true);
//             p4.SetActive(true);
//             p5.SetActive(true);
//             p6.SetActive(false);
//             p7.SetActive(false);
//             p8.SetActive(false);
//             _source.volume = 0.6f;
//         }
//         if (targetTime >= 6.0f)
//         {
//             p1.SetActive(true);
//             p2.SetActive(true);
//             p3.SetActive(true);
//             p4.SetActive(true);
//             p5.SetActive(true);
//             p6.SetActive(true);
//             p7.SetActive(false);
//             p8.SetActive(false);
//             _source.volume = 0.8f;
//         }
//         if (targetTime >= 7.0f)
//         {
//             p1.SetActive(true);
//             p2.SetActive(true);
//             p3.SetActive(true);
//             p4.SetActive(true);
//             p5.SetActive(true);
//             p6.SetActive(true);
//             p7.SetActive(true);
//             p8.SetActive(false);
//             _source.volume = 0.8f;
//         }
//         if (targetTime >= 8.0f)
//         {
//             p1.SetActive(true);
//             p2.SetActive(true);
//             p3.SetActive(true);
//             p4.SetActive(true);
//             p5.SetActive(true);
//             p6.SetActive(true);
//             p7.SetActive(true);
//             p8.SetActive(true);
//             _source.volume = 1.0f;
//         }
//         if (Input.GetKey(KeyCode.Space))
//         {
//             rb.AddForce(Vector3.up*3, ForceMode.Impulse);
//         }
//
//     }
//
//     public void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.CompareTag("fish"))
//         {
//             onFish = true;
//         }
//     }
//     public void OnTriggerExit(Collider other)
//     {
//         if (other.gameObject.CompareTag("fish"))
//         {
//             onFish = false;
//         }
//     }
// }