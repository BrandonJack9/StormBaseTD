using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorProjectile : MonoBehaviour
{
    private Transform target;               // Target to move towards
    public float speed = 10f;                // Speed of the projectile
    public float rotateSpeed = 200f;        // Optional: Rotation speed for spinning meteor effect
    public GameObject impactEffect;         // Effect to spawn on impact
    public static bool stopAllMeteors = false; // Static boolean to control if all meteors should stop


    void OnTriggerEnter(Collider other)
    {
        // Check if the meteor has hit the target (or any object you want)
        if (other.CompareTag("base")) // Ensure the target has the "Target" tag
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (stopAllMeteors)
        {
            Destroy(gameObject);
            return;
        }
        // Move the projectile if there's a valid target
        if (target != null)
        {
            // Direction to the target
            Vector3 direction = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;


            transform.Translate(direction.normalized * distanceThisFrame, Space.World);

            // Optional: Rotate the projectile for a meteor-like spinning effect
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }

    }

    // Set the target for this projectile
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

}