using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorLootSpawner : MonoBehaviour

{
    // Array to hold different loot prefabs
    public GameObject[] lootPrefabs;
    public MeteorProjectile script;
    // Optional spawn offset
    public Vector3 spawnOffset;
    public float dropChance = 5f;
    public bool spawned = false;

    // OnDestroy drops powerup based on random roll
    private void Start()
    {
        script = gameObject.GetComponent<MeteorProjectile>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("lazer") && script.currentHealth<1 && !spawned){
            float randomRoll = Random.Range(0f, 100f);
            if (randomRoll <= dropChance)
            {
                // Choose a random loot prefab
                int randomIndex = Random.Range(0, lootPrefabs.Length);
                GameObject selectedLoot = lootPrefabs[randomIndex];
                Instantiate(selectedLoot, transform.position + spawnOffset, Quaternion.identity);
            }
            spawned = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("lazer") && script.currentHealth < 1 && !spawned)
        {
            float randomRoll = Random.Range(0f, 100f);
            if (randomRoll <= dropChance)
            {
                // Choose a random loot prefab
                int randomIndex = Random.Range(0, lootPrefabs.Length);
                GameObject selectedLoot = lootPrefabs[randomIndex];
                Instantiate(selectedLoot, transform.position + spawnOffset, Quaternion.identity);
            }
            spawned = true;
        }
    }

}