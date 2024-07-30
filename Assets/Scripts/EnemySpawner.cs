using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float minSpawnRadius = 8.0f;
    [SerializeField] private float maxSpawnRadius = 10.0f;
    [SerializeField] private Transform towerTransform;

    [Header("Difficulty Settings")]
    [SerializeField] private float initialMinSpawnInterval = 2f;
    [SerializeField] private float initialMaxSpawnInterval = 3f;
    [SerializeField] private int initialEnemiesPerWave = 5;
    [SerializeField] private float difficultyIncreaseInterval = 30f;
    [SerializeField] private float spawnIntervalDecreasePerLevel = 0.2f;
    [SerializeField] private float prefabSpawnChanceIncreasePerLevel = 0.02f;

    private bool isSpawning = true;
    private float timeSinceLastDifficultyIncrease = 0f;
    private float currentSpawnInterval;
    private int currentEnemiesPerWave;
    private float currentPrefabSpawnChance = 0.05f; // Initial low chance

    void Start()
    {
        if (towerTransform == null)
        {
            Debug.LogError("Tower Transform is not assigned in the EnemySpawner script.");
            return;
        }

        currentSpawnInterval = initialMaxSpawnInterval;
        currentEnemiesPerWave = initialEnemiesPerWave;

        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        timeSinceLastDifficultyIncrease += Time.deltaTime;

        if (timeSinceLastDifficultyIncrease >= difficultyIncreaseInterval)
        {
            IncreaseDifficulty();
            timeSinceLastDifficultyIncrease = 0f;
        }
    }

    private void IncreaseDifficulty()
    {
        // Adjust difficulty parameters based on the level or time
        currentSpawnInterval = Mathf.Max(initialMinSpawnInterval / 2f, currentSpawnInterval - spawnIntervalDecreasePerLevel);
        currentEnemiesPerWave++;
        currentPrefabSpawnChance = Mathf.Min(0.25f, currentPrefabSpawnChance + prefabSpawnChanceIncreasePerLevel);
    }

    IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(currentSpawnInterval);

            for (int i = 0; i < currentEnemiesPerWave; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();

                GameObject enemyToSpawn = enemyPrefabs[0]; // Default to the first prefab
                if (Random.value < currentPrefabSpawnChance && enemyPrefabs.Length > 1)
                {
                    // Low chance to choose a different prefab
                    int randomIndex = Random.Range(1, enemyPrefabs.Length);
                    enemyToSpawn = enemyPrefabs[randomIndex];
                }

                Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float angle = Random.Range(0f, 360f);
        float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
        return towerTransform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
