// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorldController.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;

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

    public static float[,] MapBuildingGrid;

    public MeshFilter WorldMeshFilter;
    public MeshRenderer WorldMeshRender;

    public MeshFilter WaterMeshFilter;
    public MeshRenderer WaterMeshRender;

    public MeshCollider MeshColiderWorld;
    
    public TerrainData TerrainData;
    public Material TerrainMaterial;
    public NoiseData NoiseData;
    public TextureData TextureData;

    [Range(0,.6f)]
    public float WaterHeight;
    public int LevelOfDetail;

    public GameObject Teste;

    private float[,] _falloutMap;
    private float[,] _noiseMap;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("You can't have 2 World Controllers");
        }
        Instance = this;
    }

    private void Start()
    {
        NoiseData.Seed = GameController.Instance.seed;
        GenerateMap();
        MapBuildingGrid = BuildingGrid.GenerateBuildingGrid(MapChunkSize * 10, _noiseMap);
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
        DrawWaterMesh(MeshGenerator.GenerateTerrainMesh(_noiseMap, LevelOfDetail,WaterHeight));
        TreePlacement();
    }

    public void TreePlacement()
    {
        
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