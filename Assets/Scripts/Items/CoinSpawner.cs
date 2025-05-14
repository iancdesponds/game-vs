using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab do coin
    public Transform player;       // Referência ao jogador
    public float spawnRate = 2f;    // Intervalo entre spawns
    public float spawnDistance = 10f; // Distância mínima do jogador (fora da câmera)

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnCoin();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnCoin()
    {
        Vector2 spawnPosition = GetSpawnPosition();
        GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
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
