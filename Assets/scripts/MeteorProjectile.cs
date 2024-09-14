using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteorProjectile : MonoBehaviour
{
    private Transform target;               // Target to move towards
    public float speed = 10f;                // Speed of the projectile
    public float rotateSpeed = 200f;        // Optional: Rotation speed for spinning meteor effect
    public GameObject impactEffect;         // Effect to spawn on impact
    public static bool stopAllMeteors = false; // Static boolean to control if all meteors should stop
    public float maxHealth;
    float currentHealth;
    public Image healthBar;
    public GameObject healthBarObject;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the meteor has hit the target (or any object you want)
        if (other.CompareTag("base"))// Ensure the target has the "Target" tag
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("lazer"))
        {
            TakeDamage(1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("lazer"))
        {
            TakeDamage(.1f);
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
            transform.LookAt(target.position);
            // Optional: Rotate the projectile for a meteor-like spinning effect
            //transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Set the target for this projectile
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

}