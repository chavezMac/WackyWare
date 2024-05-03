using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent; 

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;

    public bool walkPointSet;

    public float walkPointRange;

    public float sightRange;
    public bool playerInSightRange;
    public float enemyDistanceRun = 5f;

    public bool tagged;

    //Get the game object's tag
    public string tag;

    [Header("Animator")]
    [SerializeField]
    private Animator animator;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        tag = gameObject.tag;
        tagged = false;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if(playerInSightRange) Runaway();
        if(!playerInSightRange) Patrolling();

    }
    private void Patrolling()
    {
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        float forward = Vector3.Dot(distanceToWalkPoint, transform.forward);
        animator.SetFloat("Forward", forward);

        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomz = Random.Range(-walkPointRange, walkPointRange);
        float randomx = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomz);
        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void Runaway()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < enemyDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - player.position;

            Vector3 newPos = transform.position + dirToPlayer;
            float forward = Vector3.Dot(dirToPlayer, transform.forward);
            animator.SetFloat("Forward", forward);

            agent.SetDestination(newPos);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}