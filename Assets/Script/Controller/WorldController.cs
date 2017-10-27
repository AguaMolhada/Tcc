// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorldController.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class WorldController : MonoBehaviour
{

    public static WorldController Instance { get; protected set; }
    public static int MapChunkSize
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<WorldController>();
            }
            return Instance.TerrainData.FlatShading ? 95 : 239;
        }
    }

    public static int[,] MapBuildingGrid;

    public MeshFilter WorldMeshFilter;
    public MeshRenderer WorldMeshRender;

    public MeshFilter WaterMeshFilter;
    public MeshRenderer WaterMeshRender;

    public MeshCollider MeshColiderWorld;
    
    public TerrainData TerrainData;
    public Material TerrainMaterial;
    public NoiseData NoiseData;
    public TextureData TextureData;

    public int LevelOfDetail;

    public GameObject Teste;

    private float[,] _falloutMap;
    private float[,] _noiseMap;

    private void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("You can't have 2 World Controllers");
        }
        Instance = this;
        GenerateMap();
        GenerateBuildingGridMap();
    }

    public void GenerateMap()
    {
        if (TerrainData.UseFallout)
        {
            _falloutMap = FalloffGenerator.GenerateFalloffMap(MapChunkSize);
        }
        _noiseMap = Noise.GenerateNoiseMap(MapChunkSize, MapChunkSize, NoiseData.Seed, NoiseData.NoiseScale, NoiseData.Octaves, NoiseData.Persistence, NoiseData.Lacunarity, NoiseData.Offset);

        for (var y = 0; y < MapChunkSize; y++)
        {
            for (var x = 0; x < MapChunkSize; x++)
            {
                if (TerrainData.UseFallout)
                {
                    _noiseMap[x, y] = _noiseMap[x, y] - _falloutMap[x, y];
                }
            }
        }

        TextureData.UpdateMeshHeights(TerrainMaterial, TerrainData.MinHeight, TerrainData.MaxHeight);
        TextureData.ApplyToMaterial(TerrainMaterial);
        DrawWorldMesh(MeshGenerator.GenerateTerrainMesh(_noiseMap, TerrainData.MeshHeightMultiplier, TerrainData.MeshHeightCurve, LevelOfDetail, TerrainData.FlatShading));
        DrawWaterMesh(MeshGenerator.GenerateTerrainMesh(_noiseMap, LevelOfDetail));

    }

    public void GenerateBuildingGridMap()
    {
        var count = 0;
        MapBuildingGrid = new int[MapChunkSize * 10, MapChunkSize * 10];
        _noiseMap = Noise.GenerateNoiseMap(MapChunkSize * 10, MapChunkSize * 10, NoiseData.Seed, NoiseData.NoiseScale, NoiseData.Octaves, NoiseData.Persistence, NoiseData.Lacunarity, NoiseData.Offset);

        for (int y = 0; y < MapChunkSize*4; y++)
        {
            for (int x = 0; x < MapChunkSize*4; x++)
            {
                if (_noiseMap[x, y] > 0.46f)
                {
                    MapBuildingGrid[x, y] = 0;
                    count++;
                    if (count < 100)
                    {
                      // TODO WORK MORE   Instantiate(Teste, new Vector3(x/10f, y/10f), Quaternion.identity);
                    }
                }
                else
                {
                    MapBuildingGrid[x, y] = -1;
                }
            }
        }

    }

    public void DrawWorldMesh(MeshData meshdata)
    {
        WorldMeshFilter.sharedMesh = meshdata.CreateMesh();
        MeshColiderWorld.sharedMesh = meshdata.CreateMesh();
    }

    public void DrawWaterMesh(MeshData meshdata)
    {
        WaterMeshFilter.sharedMesh = meshdata.CreateMesh();
    }

}