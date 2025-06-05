using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public ObjectPool enemyPool;
    public Transform player;
    public float spawnRate = 2f;
    public float spawnDistance = 10f;

    private float nextSpawnTime;

    // Difficulty scaling
    private float speedMultiplier = 1f;
    private float spawnRateMultiplier = 1f;
    private float nextDifficultyIncreaseTime = 10f;
    private const float difficultyInterval = 10f;
    private const float maxMultiplierSpeed = 3f;
    private const float maxMultiplierSpawn = 20f;
    private const float multiplierStep = 0.1f;

    void Update()
    {
        // Handle spawning
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + (spawnRate / spawnRateMultiplier);
        }

        // Handle difficulty scaling
        if (Time.time >= nextDifficultyIncreaseTime)
        {
            IncreaseDifficulty();
            nextDifficultyIncreaseTime += difficultyInterval;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = GetSpawnPosition();

        GameObject enemy = enemyPool.GetObject();
        enemy.transform.position = spawnPosition;
        enemy.transform.rotation = Quaternion.identity;

        var follow = enemy.GetComponent<EnemyFollowAttack>();
        follow.player = player;
        follow.SetSpeedMultiplier(speedMultiplier); // Apply speed scaling

        var health = enemy.GetComponent<EnemyHealth>();
        health.ResetHealth();
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 playerPosition = player.position;
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 spawnOffset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnDistance;
        return playerPosition + spawnOffset;
    }

    void IncreaseDifficulty()
    {
        if (spawnRateMultiplier < maxMultiplierSpawn)
        {
            spawnRateMultiplier = Mathf.Min(spawnRateMultiplier + multiplierStep*5, maxMultiplierSpawn);
        }

        if (speedMultiplier < maxMultiplierSpeed)
        {
            speedMultiplier = Mathf.Min(speedMultiplier + multiplierStep, maxMultiplierSpeed);
        }

        Debug.Log($"Difficulty increased! SpawnRate x{spawnRateMultiplier}, Speed x{speedMultiplier}");
    }
}
