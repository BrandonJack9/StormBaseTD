using UnityEngine;

public class EnemyDropPowerup : MonoBehaviour
{
    // List of power-up prefabs to potentially drop
    public GameObject[] powerUpPrefabs;

    // Chance for a power-up to drop (0 to 1, e.g. 0.1f for 10% chance)
    [Range(0, 1)]
    public float dropChance = 0.1f;

    // Enemy's health
    public float health = 100;

    // Called when enemy takes damage or dies
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        DropPowerUp();
        Destroy(gameObject); 
    }

    // Randomly decides whether to drop a power-up
    private void DropPowerUp()
    {
        // Roll a random number between 0 and 1
        float randomChance = Random.Range(0f, 1f);

        // If the random number is within the dropChance, drop a power-up
        if (randomChance <= dropChance && powerUpPrefabs.Length > 0)
        {
            // Select a random power-up from the array
            int randomIndex = Random.Range(0, powerUpPrefabs.Length);
            GameObject powerUpToDrop = powerUpPrefabs[randomIndex];

            // Instantiate the power-up at the enemy's position
            Instantiate(powerUpToDrop, transform.position, Quaternion.identity);
        }
    }
}

