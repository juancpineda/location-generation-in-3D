//----------------------------------------------------------------------------------------------------------------------------------------
// Chunk
// This class represents a chunk of the world, which is a portion of terrain with obstacles and decorative elements.
// It has methods to generate and destroy the chunk based on position and world.
//----------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public static int size = 16;

    public WorldGenerator world;

    public Vector2Int position;

    public List<GameObject> obstacles;

    public List<GameObject> decorations;

    public GameObject chunkCollition;

    public void Generate()
    {
        TerrainType terrain = world.GetTerrainType(position);

        GetComponent<Renderer>().material = terrain.material;

        chunkCollition.SetActive(terrain.isObstacleChunk);

        if (!terrain.isObstacleChunk)
        {
            GenerateAssets(terrain);
        }
    }

    private void GenerateAssets(TerrainType terrain)
    {
        obstacles = new List<GameObject>();


        decorations = new List<GameObject>();

        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                Vector3 worldPos = new Vector3(position.x * size + x, 0, position.y * size + z);

                float noise = Mathf.PerlinNoise(worldPos.x * terrain.noiseScale, worldPos.z * terrain.noiseScale);

                if (noise > terrain.obstacleThreshold)
                {
                    GameObject obstacle = terrain.obstacles[UnityEngine.Random.Range(0, terrain.obstacles.Length)];

                    GameObject instance = Instantiate(obstacle, worldPos, Quaternion.identity, transform);
                    obstacles.Add(instance);
                }
                else if (noise > terrain.decorationThreshold)
                {
                    GameObject decoration = terrain.decorations[UnityEngine.Random.Range(0, terrain.decorations.Length)];

                    GameObject instance = Instantiate(decoration, worldPos, Quaternion.identity, transform);
                    decorations.Add(instance);
                }
            }
        }

    }

    public void Destroy()
    {
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }

        foreach (GameObject decoration in decorations)
        {
            Destroy(decoration);
        }

        Destroy(gameObject);
    }
}