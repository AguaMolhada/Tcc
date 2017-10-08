// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorldGeneratorController.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using UnityEngine;
using UnityEngine.TestTools;

public class WorldGeneratorController : MonoBehaviour
{
    private static WorldGeneratorController _instance;
    public static int MapChunkSize { get
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<WorldGeneratorController>();
        }
        return _instance.TerrainData.FlatShading ? 95 : 239;
    } }

    public TerrainData TerrainData;
    public Material TerrainMaterial;
    public NoiseData NoiseData;
    public TextureData TextureData;

    public int LevelOfDetail;
    public TerrainType[] Regions;

    private float[,] _falloutMap;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        if ( TerrainData.UseFallout )
        {
            _falloutMap = FalloffGenerator.GenerateFalloffMap(MapChunkSize);
        }
        var noiseMap = Noise.GenerateNoiseMap(MapChunkSize, MapChunkSize , NoiseData.Seed, NoiseData.NoiseScale , NoiseData.Octaves , NoiseData.Persistence , NoiseData.Lacunarity , NoiseData.Offset);

        for (var y = 0; y < MapChunkSize; y++)
        {
            for (var x = 0; x < MapChunkSize; x++)
            {
                if ( TerrainData.UseFallout )
                {
                    noiseMap[x, y] = noiseMap[x, y] - _falloutMap[x, y];
                }
            }
        }

        TextureData.UpdateMeshHeights(TerrainMaterial,TerrainData.MinHeight,TerrainData.MaxHeight);
        TextureData.ApplyToMaterial(TerrainMaterial);
        WorldController world = FindObjectOfType<WorldController>();
        world.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, TerrainData.MeshHeightMultiplier,TerrainData.MeshHeightCurve, LevelOfDetail, TerrainData.FlatShading));

    }
}

[System.Serializable]
public struct TerrainType
{
    public string Name;
    public float Height;
    public Color TerrainColor;
}
