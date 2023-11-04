//----------------------------------------------------------------------------------------------------------------------------------------
// WorldGenerator
// This class represents the world generator, which is responsible for creating and eliminating the pieces around the player according to the viewing distance.
// It has a reference to the player, the chunk prefab and the possible terrain types.
//----------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public int viewDistance = 4;

    public Transform player;

    public GameObject chunkPrefab;

    public TerrainType[] terrainTypes;

    private Dictionary<Vector2Int, Chunk> chunks;

    private Vector2Int playerChunkPos;

    private void Start()
    {
        chunks = new Dictionary<Vector2Int, Chunk>();

        playerChunkPos = GetChunkPosition(player.position);

        GenerateChunks();
    }

    private void Update()
    {
        Vector2Int currentChunkPos = GetChunkPosition(player.position);

        if (currentChunkPos != playerChunkPos)
        {
            playerChunkPos = currentChunkPos;

            GenerateChunks();
        }
    }

    private void GenerateChunks()
    {
        List<Vector2Int> positionsToGenerate = new List<Vector2Int>();

        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int y = -viewDistance; y <= viewDistance; y++)
            {
                Vector2Int position = playerChunkPos + new Vector2Int(x, y);

                if (!chunks.ContainsKey(position))
                {
                    positionsToGenerate.Add(position);
                }
            }
        }

        positionsToGenerate.Sort((a, b) => Vector2Int.Distance(a, playerChunkPos).CompareTo(Vector2Int.Distance(b, playerChunkPos)));

        foreach (Vector2Int position in positionsToGenerate)
        {
            GenerateChunk(position);
        }

        List<Vector2Int> positionsToDestroy = new List<Vector2Int>();

        foreach (Vector2Int position in chunks.Keys)
        {
            if (Vector2Int.Distance(position, playerChunkPos) > viewDistance)
            {
                positionsToDestroy.Add(position);
            }
        }

        foreach (Vector2Int position in positionsToDestroy)
        {
            DestroyChunk(position);
        }
    }

    private void GenerateChunk(Vector2Int position)
    {
        GameObject instance = Instantiate(chunkPrefab, new Vector3(position.x * Chunk.size, 0, position.y * Chunk.size), Quaternion.identity, transform);

        Chunk chunk = instance.GetComponent<Chunk>();

        chunk.world = this;

        chunk.position = position;

        chunk.Generate();

        chunks.Add(position, chunk);
    }

    private void DestroyChunk(Vector2Int position)
    {
        Chunk chunk = chunks[position];

        chunk.Destroy();

        chunks.Remove(position);
    }

    private Vector2Int GetChunkPosition(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / Chunk.size);
        int y = Mathf.RoundToInt(worldPos.z / Chunk.size);

        return new Vector2Int(x, y);
    }

    public TerrainType GetTerrainType(Vector2Int position)
    {
        float noise = Mathf.PerlinNoise(position.x * 0.1f, position.y * 0.1f);

        if (noise < 0.3f)
        {
            return terrainTypes[0];
        }
        else if (noise < 0.6f)
        {
            return terrainTypes[1];
        }
        else
        {
            return terrainTypes[2];
        }
    }
}