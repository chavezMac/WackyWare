using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
   
    Transform target;

    Vector3 moveDirection;

    private FieldOfView fieldOfView;



    private void Start()
    {
        target = GameObject.Find("ThirdPersonPlayer").transform;
        fieldOfView = GetComponent<FieldOfView>();
    }

    private void Update()
    {
        if (fieldOfView && fieldOfView.canSeePlayer)
        {
            if (target)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            }
        }
    }
  
}
