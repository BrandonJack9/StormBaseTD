using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject projectilePrefab1, projectilePrefab2, projectilePrefab3, enemyPrefab1, enemyPrefab2, enemyPrefab3;  // The projectile prefab (e.g., the meteor)
    public Transform MeteorSpawnerBase;         // The base of the spawner, used for positioning (it should be right where the player base is)
    public Transform target;             // The target (e.g., the player)
    public float fireRateAir1, fireRateAir2, fireRateAir3, fireRateGround1, fireRateGround2, fireRateGround3;          // Time between shots
    public float spawnRadius = 10f;      // The maximum radius distance to spawn the meteor
    public float minSpawnDistance = 5f;  // The minimum distance away from the MeteorSpawner to spawn
    public float spawnHeightAir = 10f;      // The fixed height (Y position) where the meteor spawns
    public float spawnHeightGround = 10f;      // The fixed height (Y position) where the enemy spawns


    private float fireCooldownAir = 0f;
    private float fireCooldownAir2 = 0f;
    private float fireCooldownAir3 = 0f;
    private float fireCooldownGround = 0f;
    private float fireCooldownGround2 = 0f;
    private float fireCooldownGround3 = 0f;
    private bool canSpawnAir = true;        // Controls whether meteors can spawn
    private bool canSpawnGround = true;     // Controls whether meteors can spawn

    void Update()
    {
        // Shoot at intervals
        if (canSpawnAir && fireCooldownAir <= 0f)
        {
            SpawnMeteor(projectilePrefab1);
            fireCooldownAir = 1f / fireRateAir1;  // Reset cooldown
        }

        if (canSpawnAir && fireCooldownAir2 <= 0f)
        {
            SpawnMeteor(projectilePrefab2);
            fireCooldownAir2 = 1f / fireRateAir2;  // Reset cooldown
        }

        if (canSpawnAir && fireCooldownAir3 <= 0f)
        {
            SpawnMeteor(projectilePrefab3);
            fireCooldownAir3 = 1f / fireRateAir3;  // Reset cooldown
        }

        if (canSpawnGround && fireCooldownGround <= 0f)
        {
            SpawnGroundEnemy(enemyPrefab1);
            fireCooldownGround = 1f / fireRateGround1;
        }

        if (canSpawnGround && fireCooldownGround2 <= 0f)
        {
            SpawnGroundEnemy(enemyPrefab2);
            fireCooldownGround2 = 1f / fireRateGround2;
        }

        if (canSpawnGround && fireCooldownGround3 <= 0f)
        {
            SpawnGroundEnemy(enemyPrefab3);
            fireCooldownGround3 = 1f / fireRateGround3;
        }

        // Decrease cooldown over time
        fireCooldownAir -= Time.deltaTime;
        fireCooldownAir2 -= Time.deltaTime;
        fireCooldownAir3 -= Time.deltaTime;
        fireCooldownGround -= Time.deltaTime;
        fireCooldownGround3 -= Time.deltaTime;
        fireCooldownGround2 -= Time.deltaTime;
    }

    void SpawnMeteor(GameObject meteor)
    {
        // Get a random spawn position in a circle around the turret, within certain bounds
        Vector3 spawnPosition = GetRandomSpawnPositionAir();

        // Instantiate the meteor (projectile) at the calculated random position
        GameObject projectile = Instantiate(meteor, spawnPosition, Quaternion.identity);

        // Set the target for the projectile
        projectile.GetComponent<MeteorProjectile>().SetTarget(target);
    }

    void SpawnGroundEnemy(GameObject enemy)
    {
        Vector3 spawnPosition = GetRandomSpawnPositionGround();

        // Instantiate the meteor (projectile) at the calculated random position
        GameObject projectile = Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

    // Method to calculate a random spawn position at a fixed height
    Vector3 GetRandomSpawnPositionAir()
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
        Vector3 spawnPosition = new Vector3(basePosition.x + xOffset, spawnHeightAir, basePosition.z + zOffset);

        return spawnPosition;
    }

    Vector3 GetRandomSpawnPositionGround()
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
        Vector3 spawnPosition = new Vector3(basePosition.x + xOffset, spawnHeightGround, basePosition.z + zOffset);

        return spawnPosition;
    }
    public void StopSpawning()
    {
        canSpawnAir = false;
        canSpawnGround = false;
        //MeteorProjectile.stopAllMeteors = true;
    }
    
    public void StartSpawning()
    {
        canSpawnAir = true;
        canSpawnGround = false;
        //MeteorProjectile.stopAllMeteors = false;
    }
}