using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float minSpawnInterval = 1.0f;
    public float maxSpawnInterval = 3.0f;
    public float minSpawnRadius = 8.0f;
    public float maxSpawnRadius = 10.0f;
    public Transform towerTransform;
    public int minEnemiesPerWave = 1;
    public int maxEnemiesPerWave = 5;
    public float[] spawnProbabilities; // Array to hold spawn probabilities for each prefab

    private bool isSpawning = true;

    void Start()
    {
        if (towerTransform == null)
        {
            Debug.LogError("Tower Transform is not assigned in the EnemySpawner script.");
            return;
        }

        // Ensure spawn probabilities array matches the number of enemy prefabs
        if (spawnProbabilities.Length != enemyPrefabs.Length)
        {
            Debug.LogError("Spawn probabilities array length must match enemy prefabs array length.");
            return;
        }

        StartCoroutine(SpawnEnemies());
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            int enemiesToSpawn = Random.Range(minEnemiesPerWave, maxEnemiesPerWave + 1);
            List<Vector3> spawnPositions = new List<Vector3>(); // List to keep track of spawn positions
            HashSet<Vector3> usedPositions = new HashSet<Vector3>(); // To keep track of used positions

            // Generate spawn positions for the first prefab
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Vector3 spawnPosition = GetRandomPointOnCircle(minSpawnRadius, maxSpawnRadius, towerTransform.position, usedPositions);
                spawnPositions.Add(spawnPosition);
                usedPositions.Add(spawnPosition);
            }

            // Attempt to replace some spawn positions with other prefabs based on spawn probabilities
            for (int j = 1; j < enemyPrefabs.Length; j++)
            {
                for (int i = 0; i < spawnPositions.Count; i++)
                {
                    if (Random.value < spawnProbabilities[j])
                    {
                        spawnPositions[i] = Vector3.zero; // Mark position for replacement
                        break;
                    }
                }
            }

            // Instantiate enemies at the calculated positions
            for (int i = 0; i < spawnPositions.Count; i++)
            {
                if (spawnPositions[i] == Vector3.zero)
                {
                    // Replace with a different prefab based on probabilities
                    int enemyIndex = GetRandomPrefabIndex();
                    if (enemyIndex >= 1)
                    {
                        Vector3 spawnPosition = GetRandomPointOnCircle(minSpawnRadius, maxSpawnRadius, towerTransform.position, usedPositions);
                        usedPositions.Add(spawnPosition);
                        Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
                    }
                }
                else
                {
                    // Spawn the first prefab
                    Instantiate(enemyPrefabs[0], spawnPositions[i], Quaternion.identity);
                }
            }
        }
    }

    int GetRandomPrefabIndex()
    {
        float totalProbability = 0f;
        for (int i = 1; i < spawnProbabilities.Length; i++)
        {
            totalProbability += spawnProbabilities[i];
        }

        float randomPoint = Random.value * totalProbability;
        for (int i = 1; i < spawnProbabilities.Length; i++)
        {
            if (randomPoint < spawnProbabilities[i])
            {
                return i;
            }
            else
            {
                randomPoint -= spawnProbabilities[i];
            }
        }
        return 0;
    }

    Vector3 GetRandomPointOnCircle(float minRadius, float maxRadius, Vector3 center, HashSet<Vector3> usedPositions)
    {
        Vector3 spawnPosition;
        int attempts = 0;
        do
        {
            float radius = Random.Range(minRadius, maxRadius);
            float angle = Random.Range(0f, 360f);
            float x = center.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = center.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            float z = center.z;
            spawnPosition = new Vector3(x, y, z);
            attempts++;
        } while (usedPositions.Contains(spawnPosition) && attempts < 100); // Limit attempts to avoid infinite loop

        return spawnPosition;
    }
}
