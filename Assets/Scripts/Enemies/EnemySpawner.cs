using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab do inimigo
    public Transform player;       // Referência ao jogador
    public float spawnRate = 2f;    // Intervalo entre spawns
    public float spawnDistance = 10f; // Distância mínima do jogador (fora da câmera)

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = GetSpawnPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<EnemyFollowAttack>().player = player;
    }


    Vector2 GetSpawnPosition()
    {
        Vector2 playerPosition = player.position;

        // Gera um ângulo aleatório
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        // Calcula uma posição ao redor do jogador, fora da área da câmera
        Vector2 spawnOffset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnDistance;

        return playerPosition + spawnOffset;
    }
}
