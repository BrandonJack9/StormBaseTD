using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLootSpawner : MonoBehaviour

{
    // Array to hold different loot prefabs
    public GameObject[] lootPrefabs;

    // Optional spawn offset
    public Vector3 spawnOffset;

    public float dropChance = 5f;

    // OnDestroy drops powerup based on random roll
    private void OnDestroy()
    {
        float randomRoll = Random.Range(0f, 100f);
        if(randomRoll <= dropChance){
            if (lootPrefabs.Length == 0)
            {
                Debug.LogWarning("No loot prefabs assigned.");
                return;
            }

            // Choose a random loot prefab
            int randomIndex = Random.Range(0, lootPrefabs.Length);
            GameObject selectedLoot = lootPrefabs[randomIndex];

            Instantiate(selectedLoot, transform.position + spawnOffset, Quaternion.identity);
        }
    }
}

