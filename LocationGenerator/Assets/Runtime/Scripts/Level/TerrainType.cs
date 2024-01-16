//----------------------------------------------------------------------------------------------------------------------------------------
// TerrainType
// This class represents a terrain type, which defines the material, noise, threshold, and possible obstacles and decorative elements for the terrain.
// It is a ScriptableObject that can be created from the Unity editor.
//----------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainType", menuName = "ScriptableObjects/TerrainType", order = 1)]
public class TerrainType : ScriptableObject
{
    public bool isObstacleChunk = false;

    public Material material;

    public float noiseScale;

    public float obstacleThreshold;

    public float decorationThreshold;

    public GameObject[] obstacles;

    public GameObject[] decorations;
}

