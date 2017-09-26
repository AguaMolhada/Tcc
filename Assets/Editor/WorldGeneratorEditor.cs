using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

[CustomEditor(typeof(WorldGeneratorController))]
public class WorldGeneratorEditor : Editor
{

    private WorldGeneratorController _mapController;
    private bool _custom;
    private bool _autoUpdate;
    private bool _regions;
    private bool _colors;
    private bool _height;

    public override void OnInspectorGUI()
    {

        _mapController = (WorldGeneratorController) target;
        _autoUpdate = EditorGUILayout.Toggle("Auto Update Mesh", _autoUpdate);
        if (GUILayout.Button("Change Inspector"))
        {
            _custom = !_custom;
        }
        if (_custom)
        {
            EditorGUILayout.LabelField("Map Proprieties");
            _mapController.TerrainData.FlatShading = EditorGUILayout.Toggle("Use Flat Shading (Here only to test)",
                _mapController.TerrainData.FlatShading);
            _mapController.LevelOfDetail = EditorGUILayout.IntSlider("LOD", _mapController.LevelOfDetail, 0, 6);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Mesh Proprieties");
            _mapController.TerrainData.UseFallout = EditorGUILayout.Toggle("Is Map Surronded by water",
                _mapController.TerrainData.UseFallout);
            _mapController.TerrainData.MeshHeightMultiplier = EditorGUILayout.IntField("Mesh Height Multiplier",
                _mapController.TerrainData.MeshHeightMultiplier);
            _mapController.TerrainData.MeshHeightCurve = EditorGUILayout.CurveField("Influence Curve",
                _mapController.TerrainData.MeshHeightCurve);
            EditorGUILayout.LabelField("_______");
            if (GUILayout.Button("Regions"))
            {
                _regions = !_regions;
            }
            if (_regions)
            {
                Color[] tempColor = _mapController.TextureData.BaseColours;
                float[] tempHeigt = _mapController.TextureData.BaseStartHeights;
                if (GUILayout.Button("Add New Region Color"))
                {
                    _mapController.TextureData.BaseColours = new Color[tempColor.Length + 1];
                    _mapController.TextureData.BaseStartHeights = new float[tempHeigt.Length + 1];
                    for (int i = 0; i < tempColor.Length; i++)
                    {
                        _mapController.TextureData.BaseColours[i] = tempColor[i];
                    }
                    for (int i = 0; i < tempHeigt.Length; i++)
                    {
                        _mapController.TextureData.BaseStartHeights[i] = tempHeigt[i];
                    }
                }
                for (int i = 0; i < _mapController.TextureData.BaseColours.Length; i++)
                {
                    
                    _mapController.TextureData.BaseColours[i] = EditorGUILayout.ColorField("Region Color",_mapController.TextureData.BaseColours[i]);
                    if (i > 0 && i <_mapController.TextureData.BaseColours.Length-1)
                    {
                        _mapController.TextureData.BaseStartHeights[i] = EditorGUILayout.Slider("Terrain Height",
                            _mapController.TextureData.BaseStartHeights[i],
                            _mapController.TextureData.BaseStartHeights[i - 1],
                            _mapController.TextureData.BaseStartHeights[i + 1]);
                    }
                    else if (i == 0)
                    {
                        _mapController.TextureData.BaseStartHeights[i] = EditorGUILayout.Slider("Terrain Height",
                            _mapController.TextureData.BaseStartHeights[i], 0,
                            _mapController.TextureData.BaseStartHeights[i + 1]);
                    }
                    else
                    {
                        _mapController.TextureData.BaseStartHeights[i] = EditorGUILayout.Slider("Terrain Height",
                            _mapController.TextureData.BaseStartHeights[i],
                            _mapController.TextureData.BaseStartHeights[i - 1],
                            1);

                    }
                    EditorGUILayout.Space();
                }
                if (GUILayout.Button("Remove Region Color"))
                {
                    _mapController.TextureData.BaseColours = new Color[tempColor.Length - 1];
                    _mapController.TextureData.BaseStartHeights = new float[tempHeigt.Length - 1];
                    for (int i = 0; i < tempColor.Length - 1; i++)
                    {
                        _mapController.TextureData.BaseColours[i] = tempColor[i];
                    }
                    for (int i = 0; i < tempHeigt.Length; i++)
                    {
                        _mapController.TextureData.BaseStartHeights[i] = tempHeigt[i];
                    }
                }


            }

            EditorGUILayout.LabelField("Noise Values");
            _mapController.NoiseData.NoiseScale = EditorGUILayout.FloatField("Noise",
                _mapController.NoiseData.NoiseScale);
            _mapController.NoiseData.Octaves = EditorGUILayout.IntField("Octaves Numbers",
                _mapController.NoiseData.Octaves);
            _mapController.NoiseData.Persistence = EditorGUILayout.Slider("Persistence Value",
                _mapController.NoiseData.Persistence, 0,
                1);
            _mapController.NoiseData.Lacunarity = EditorGUILayout.FloatField("Lacunarity Value",
                _mapController.NoiseData.Lacunarity);
            EditorGUILayout.LabelField("_______");
            EditorGUILayout.LabelField("Other Things");
            _mapController.NoiseData.Offset = EditorGUILayout.Vector2Field("Octaves Noise Offset",
                _mapController.NoiseData.Offset);
            _mapController.NoiseData.Seed = EditorGUILayout.IntField("Map Seed:", _mapController.NoiseData.Seed);
            if (GUILayout.Button("Generate New Seed"))
            {
                _mapController.NoiseData.Seed = Ultility.GetRandomString(new Random(), 40).GetHashCode();
            }
        }
        else
        {
            base.OnInspectorGUI();
        }

        if (_autoUpdate)
        {
            _mapController.GenerateMap();
        }

    }
}