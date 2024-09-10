using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GroundEnemyMovement : MonoBehaviour
{
    public Transform target;  // The target that the enemy will follow (e.g., the player)
    public float stoppingDistance = 2f; // Distance from the target at which the enemy stops moving

    private NavMeshAgent navMeshAgent; // The NavMeshAgent component

    void Start()
    {
        // Get the NavMeshAgent component attached to the enemy
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Set the stopping distance
        navMeshAgent.stoppingDistance = stoppingDistance;
    }

    void Update()
    {
        if (target != null)
        {
            // Set the target position for the NavMeshAgent to move towards
            navMeshAgent.SetDestination(target.position);
            Debug.Log("Destination Set: " + target.position);
             if (navMeshAgent.hasPath)
            {
                Debug.Log("Moving towards target...");
            }
            else
            {
                Debug.Log("No path found.");
            }
        }

        // Rotate the enemy to face the target while maintaining the Y-axis
        if (navMeshAgent.remainingDistance > stoppingDistance)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}


