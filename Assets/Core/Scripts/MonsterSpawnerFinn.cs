using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerFinn : MonoBehaviour
{
    // Monster prefab to spawn
    public GameObject monsterPrefab;

    // Number of monsters to spawn
    public int monstersToSpawn = 5;

    // Range to detect the player for spawning
    public float spawnRange = 10f;

    // Player reference
    public Transform player;

    // Counter for how many monsters have been spawned
    private int monstersSpawned = 0;

    // Flag to track if spawning has started
    private bool spawningStarted = false;

    void Update()
    {
        // Check if the player is within range
        if (player != null && !spawningStarted)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // If the player is within range, start spawning monsters
            if (distanceToPlayer <= spawnRange)
            {
                StartSpawning();
                spawningStarted = true; // Prevent further checks after spawning starts
            }
        }

        // After spawning all monsters, disable this spawner
        if (monstersSpawned >= monstersToSpawn)
        {
            DisableSpawner();
        }
    }

    // Method to start spawning monsters
    public void StartSpawning()
    {
        // Ensure we have a valid monster prefab
        if (monsterPrefab == null)
        {
            Debug.LogError("Missing monster prefab!");
            return;
        }

        // Spawn monsters up to the specified number
        for (int i = 0; i < monstersToSpawn; i++)
        {
            SpawnMonster();
        }
    }

    // Method to spawn a single monster
    private void SpawnMonster()
    {
        // Instantiate the monster at the spawner's position
        Instantiate(monsterPrefab, transform.position, transform.rotation);

        // Increment the spawned counter
        monstersSpawned++;
    }

    // Method to disable the spawner after all monsters have been spawned
    private void DisableSpawner()
    {
        // Disable the game object or set inactive
        gameObject.SetActive(false);
    }

    // Draw spawn range in the editor for debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }
}
