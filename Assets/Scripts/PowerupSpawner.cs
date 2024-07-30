using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] powerupPrefabs; // Array to hold your different powerup prefabs
    public float minSpawnInterval = 5f;
    public float maxSpawnInterval = 15f;

    private float nextSpawnTime;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnPowerup();
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnPowerup()
    {
        // Choose a random powerup
        GameObject randomPowerup = powerupPrefabs[Random.Range(0, powerupPrefabs.Length)];

        // Get camera boundaries for spawning
        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = halfHeight * mainCamera.aspect;

        // Spawn at a random position within the camera view
        Vector3 spawnPosition = new Vector3(
            Random.Range(-halfWidth, halfWidth),
            Random.Range(-halfHeight, halfHeight),
            0f
        );

        // Instantiate the powerup
        GameObject spawnedPowerup = Instantiate(randomPowerup, spawnPosition, Quaternion.identity);

        // Add PowerupController component to the spawned powerup
        PowerupController powerupController = spawnedPowerup.AddComponent<PowerupController>();
        powerupController.lifetime = Random.Range(5f, 10f); // Random lifetime for the powerup (5 to 10 seconds)
    }
}
