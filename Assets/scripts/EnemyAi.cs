
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public GameObject crystal;


    public LayerMask whatIsGround, whatIsPlayer;

    public float maxHealth;
    float currentHealth;
    public Image healthBar;
    public GameObject healthBarObject;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange, meleeRange;
    public bool playerInSightRange, playerInAttackRange;

    //Small, Medium, Big
    public bool isSmall;
    public bool isMedium;
    public bool isLarge;

    void OnTriggerEnter(Collider other)
    {
        // Check if the meteor has hit the target (or any object you want)
        if ( other.CompareTag("lazer"))
        {
            TakeDamage(1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("lazer"))
        {
            TakeDamage(.1f * crystal.GetComponent<TurretScript>().beamStack);
        }
    }
    private void Awake()
    {
        player = GameObject.Find("basePyramid").transform;
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        crystal = GameObject.Find("turret");
    }

  

    private void Update()
    {
        //Check for sight and attack range
        if (isSmall || isLarge)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, meleeRange, whatIsPlayer);
        }
        else
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        }
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        if(currentHealth <= 0)
        {
           DestroyEnemy();
        }

        
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            if (isSmall)
            {
                HealthManager hManager = GameObject.Find("basePyramid").GetComponent<HealthManager>();
                hManager.healthAmount -= 1;
                hManager.healthBar.fillAmount = hManager.healthAmount / 100f;
                Debug.Log("Small melee attack!");

            }
            else if (isMedium)
            {
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            }
            else if (isLarge)
            {
                HealthManager hManager = GameObject.Find("basePyramid").GetComponent<HealthManager>();
                hManager.healthAmount -= 5;
                hManager.healthBar.fillAmount = hManager.healthAmount / 100f;
                Debug.Log("Large melee attack!");
            }
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
