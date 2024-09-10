using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMeteorSpawn : MonoBehaviour
{
    public GameObject projectilePrefab;  // The projectile prefab (e.g., the meteor)
    public Transform MeteorSpawnerBase;         // The base of the spawner, used for positioning (it should be right where the player base is)
    public Transform target;             // The target (e.g., the player)
    public float fireRate = 10f;          // Time between shots
    public float spawnRadius = 10f;      // The maximum radius distance to spawn the meteor
    public float minSpawnDistance = 5f;  // The minimum distance away from the MeteorSpawner to spawn
    public float spawnHeight = 10f;      // The fixed height (Y position) where the meteor spawns

    private float fireCooldown = 0f;
    private bool canSpawn = true;        // Controls whether meteors can spawn

    void Update()
    {
        // Shoot at intervals
        if (canSpawn && fireCooldown <= 0f)
        {
            SpawnMeteor();
            fireCooldown = 1f / fireRate;  // Reset cooldown
        }

        // Decrease cooldown over time
        fireCooldown -= Time.deltaTime;
    }

    void SpawnMeteor()
    {
        // Get a random spawn position in a circle around the turret, within certain bounds
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Instantiate the meteor (projectile) at the calculated random position
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        // Set the target for the projectile
        projectile.GetComponent<MeteorProjectile>().SetTarget(target);
    }

    // Method to calculate a random spawn position at a fixed height
    Vector3 GetRandomSpawnPosition()
    {
        Vector3 basePosition = MeteorSpawnerBase.position;

        // Get a random angle in radians
        float angle = Random.Range(0f, Mathf.PI * 2);

        // Get a random distance between minSpawnDistance and spawnRadius
        float distance = Random.Range(minSpawnDistance, spawnRadius);

        // Calculate the X and Z offsets using trigonometry
        float xOffset = Mathf.Cos(angle) * distance;
        float zOffset = Mathf.Sin(angle) * distance;

        // Set the final position with random X and Z, but fixed Y (height)
        Vector3 spawnPosition = new Vector3(basePosition.x + xOffset, spawnHeight, basePosition.z + zOffset);

        return spawnPosition;
    }
    public void StopSpawning()
    {
        canSpawn = false;
        MeteorProjectile.stopAllMeteors = true;
    }
    
}