using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("Chunks")]
    public List<GameObject> terrainChunks;
    public GameObject player;
    public GameObject initialChunk;

    [Header("Spawn Settings")]
    public float checkerRadius = 0.1f;
    public LayerMask terrainMask;

    [Header("Optimization")]
    public float maxOpDist;
    public float optimizerCooldownDur = 0.25f;

    // Runtime fields
    private Vector3 playerLastPosition;
    private Rigidbody2D playerRb;
    [HideInInspector] public GameObject currentChunk;
    private readonly List<GameObject> spawnedChunks = new();
    private readonly Dictionary<Vector3, GameObject> chunkMap = new();
    private float optimizerCooldown;

    // Names of the child transforms that mark neighbor positions (must exist in prefab)
    private readonly string[] directions =
    {
        "Up", "Down", "Left", "Right",
        "UpLeft", "UpRight", "DownLeft", "DownRight"
    };

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        playerLastPosition = player.transform.position;

        currentChunk = initialChunk;
        spawnedChunks.Add(initialChunk);
        chunkMap[initialChunk.transform.position] = initialChunk;
    }

    void Update()
    {
        UpdateCurrentChunk();
        SpawnNeighbors();
        ChunkOptimizer();
    }

    // --------------------------------------------------
    // 1. Descobre em qual chunk o jogador está de verdade
    // --------------------------------------------------
    void UpdateCurrentChunk()
    {
        float closest = float.MaxValue;
        GameObject nearest = currentChunk;

        foreach (GameObject chunk in spawnedChunks)
        {
            float d = Vector2.Distance(player.transform.position, chunk.transform.position);
            if (d < closest)
            {
                closest = d;
                nearest = chunk;
            }
        }

        currentChunk = nearest;
    }

    // --------------------------------------------------
    // 2. Gera vizinhos imediatos em 8 direções
    // --------------------------------------------------
    void SpawnNeighbors()
    {
        if (currentChunk == null) return;

        // só gera se mudou de chunk ou se está muito perto da borda
        foreach (string dir in directions)
        {
            CheckAndSpawnChunk(currentChunk, dir);
        }
    }

    void CheckAndSpawnChunk(GameObject sourceChunk, string pointName)
    {
        Transform cp = sourceChunk.transform.Find(pointName);
        if (cp == null)
        {
            // Prefab mal configurado
            return;
        }

        Vector3 spawnPos = cp.position;
        if (chunkMap.ContainsKey(spawnPos)) return;

        if (Physics2D.OverlapCircle(spawnPos, checkerRadius, terrainMask)) return;

        SpawnChunkAt(spawnPos);
    }

    void SpawnChunkAt(Vector3 spawnPosition)
    {
        int index = UnityEngine.Random.Range(0, terrainChunks.Count);
        GameObject newChunk = Instantiate(terrainChunks[index], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(newChunk);
        chunkMap[spawnPosition] = newChunk;
    }

    // --------------------------------------------------
    // 3. Desativa chunks longe demais para economizar
    // --------------------------------------------------
    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;
        if (optimizerCooldown > 0f) return;
        optimizerCooldown = optimizerCooldownDur;

        foreach (GameObject chunk in spawnedChunks)
        {
            float dist = Vector2.Distance(player.transform.position, chunk.transform.position);
            bool active = dist <= maxOpDist;
            if (chunk.activeSelf != active)
                chunk.SetActive(active);
        }
    }
}
