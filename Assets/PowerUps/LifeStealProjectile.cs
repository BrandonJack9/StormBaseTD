using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealProjectile : MonoBehaviour
{
    public float level = 1;
    public float healPercentage = .03f;
    public float healPercentageCap = .30f;

    public float damage = 10f;

    public GameObject healTarget;

    public GameObject parentTurret;

    public float projectileLifeTime;

    private HealthManager healthManager; // Reference to the HealthManager script

    void Start()
    {
        if (healTarget != null)
        {
            // Access the HealthManager script on the healTarget
            healthManager = healTarget.GetComponent<HealthManager>();
            if (healthManager == null)
            {
                Debug.LogError("No HealthManager script found on healTarget!");
            }
        }
    }
    //custom function for setting up healthManager
    public void SetHealTarget(GameObject target)
    {
        healTarget = target;
        healthManager = healTarget.GetComponent<HealthManager>(); // Find the HealthManager on the target
    }
    void Update()
    {
        StartCoroutine(DeleteLazer(projectileLifeTime));
    }

    IEnumerator DeleteLazer(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    // Heal when colliding with meteor
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("meteor"))
        {
            // Ensure that the health manager is available
            if (healthManager != null)
            {
                // Calculate healing amount based on healPercentage
                float healAmount = Mathf.Min(level*healPercentage, healPercentageCap)* damage;
                healthManager.Heal(healAmount); // Call the Heal method
                 Debug.Log("Healed: " +healAmount + ". Current Health: " + healthManager.healthAmount);
            }
            
            Destroy(gameObject);
        }
    }
}

