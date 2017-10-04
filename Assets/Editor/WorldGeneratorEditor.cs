using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

[CustomEditor(typeof(WorldGeneratorController))]
public class WorldGeneratorEditor : Editor {

    private WorldGeneratorController _mapController;
    private bool _custom;
    private bool _autoUpdate;
    private bool _regions;
    private bool[] _regionsNames;
    private bool _terrain;
    private bool _noise;

    public override void OnInspectorGUI() {
        GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.MaxWidth(120f) , GUILayout.MinWidth(100f) };
        _mapController = (WorldGeneratorController)target;
        _autoUpdate = EditorGUILayout.Toggle("Auto Update Mesh" , _autoUpdate);
        if ( GUILayout.Button("Change Inspector") ) {
            _custom = !_custom;
            _regionsNames = new bool[_mapController.TextureData.ColorName.Length];
        }
        if ( _custom ) {

            _terrain = EditorGUILayout.Foldout(_terrain , "TerrainData Values");
            if (_terrain)
            {
                EditorGUI.indentLevel += 1;
                _mapController.TerrainData.FlatShading = EditorGUILayout.Toggle("Flat Shading",
                    _mapController.TerrainData.FlatShading);
                _mapController.LevelOfDetail = EditorGUILayout.IntSlider("LOD", _mapController.LevelOfDetail, 0, 6);
                EditorGUILayout.Space();
                _mapController.TerrainData.UseFallout = EditorGUILayout.Toggle("Is Map Surronded by water",
                    _mapController.TerrainData.UseFallout);
                _mapController.TerrainData.MeshHeightMultiplier = EditorGUILayout.IntField("Mesh Height Multiplier",
                    _mapController.TerrainData.MeshHeightMultiplier);
                _mapController.TerrainData.MeshHeightCurve = EditorGUILayout.CurveField("Influence Curve",
                    _mapController.TerrainData.MeshHeightCurve);
                EditorGUI.indentLevel -= 1;
            }

            _regions = EditorGUILayout.Foldout(_regions , "TextureData Values");
            if ( _regions ) {
                var temp = EditorGUIUtility.labelWidth;
                EditorGUI.indentLevel += 1;
                string[] tempNames = _mapController.TextureData.ColorName;
                Color[] tempColor = _mapController.TextureData.BaseColours;
                float[] tempHeigt = _mapController.TextureData.BaseStartHeights;
                float[] tempBlend = _mapController.TextureData.BaseBlendColor;
                bool[] tempOpen = _regionsNames;
                if ( GUILayout.Button("Add New Region Color") )
                {
                    _mapController.TextureData.ColorName = new string[tempNames.Length + 1];
                    _mapController.TextureData.BaseColours = new Color[tempColor.Length + 1];
                    _mapController.TextureData.BaseStartHeights = new float[tempHeigt.Length + 1];
                    _mapController.TextureData.BaseBlendColor = new float[tempBlend.Length +1];
                    _regionsNames = new bool[tempOpen.Length + 1];
                    for ( int i = 0 ; i < tempColor.Length ; i++ ) {
                        _mapController.TextureData.BaseColours[i] = tempColor[i];
                        _mapController.TextureData.BaseStartHeights[i] = tempHeigt[i];
                        _mapController.TextureData.BaseBlendColor[i] = tempBlend[i];
                        _regionsNames[i] = tempOpen[i];
                    }
                }
                for (int i = 0; i < _mapController.TextureData.BaseColours.Length; i++)
                {
                    _regionsNames[i] = EditorGUILayout.Foldout(_regionsNames[i], _mapController.TextureData.ColorName[i]);
                    if (_regionsNames[i])
                    {
                        _mapController.TextureData.ColorName[i] = EditorGUILayout.TextArea(_mapController.TextureData.ColorName[i]);
                        EditorGUILayout.BeginHorizontal();
                        EditorGUIUtility.labelWidth = 80f;
                        _mapController.TextureData.BaseColours[i] = EditorGUILayout.ColorField("Color", _mapController.TextureData.BaseColours[i], options);
                        _mapController.TextureData.BaseBlendColor[i] = EditorGUILayout.Slider("Blend %", _mapController.TextureData.BaseBlendColor[i],0,.1f);
                        EditorGUILayout.EndHorizontal();
                        if (i > 0 && i < _mapController.TextureData.BaseColours.Length - 1)
                        {
                            _mapController.TextureData.BaseStartHeights[i] = EditorGUILayout.Slider("Height",
                                _mapController.TextureData.BaseStartHeights[i],
                                _mapController.TextureData.BaseStartHeights[i - 1],
                                _mapController.TextureData.BaseStartHeights[i + 1], GUILayout.ExpandWidth(false));
                        }
                        else if (i == 0)
                        {
                            _mapController.TextureData.BaseStartHeights[i] = EditorGUILayout.Slider("Height",
                                _mapController.TextureData.BaseStartHeights[i], 0,
                                _mapController.TextureData.BaseStartHeights[i + 1], GUILayout.ExpandWidth(false));
                        }
                        else
                        {
                            _mapController.TextureData.BaseStartHeights[i] = EditorGUILayout.Slider("Height",
                                _mapController.TextureData.BaseStartHeights[i],
                                _mapController.TextureData.BaseStartHeights[i - 1],
                                1, GUILayout.ExpandWidth(false));

                        }
                        EditorGUILayout.Space();
                    }
                }
                if ( GUILayout.Button("Remove Region Color") )
                {
                    _mapController.TextureData.ColorName = new string[tempNames.Length - 1];
                    _mapController.TextureData.BaseColours = new Color[tempColor.Length - 1];
                    _mapController.TextureData.BaseStartHeights = new float[tempHeigt.Length - 1];
                    _mapController.TextureData.BaseBlendColor = new float[tempBlend.Length - 1];
                    _regionsNames = new bool[tempOpen.Length - 1];
                    for ( int i = 0 ; i < tempColor.Length - 1 ; i++ ) {
                        _mapController.TextureData.BaseColours[i] = tempColor[i];
                        _mapController.TextureData.BaseStartHeights[i] = tempHeigt[i];
                        _mapController.TextureData.BaseBlendColor[i] = tempBlend[i];
                        _regionsNames[i] = tempOpen[i];
                    }
                }
                EditorGUI.indentLevel -= 1;
                EditorGUIUtility.labelWidth = temp;
            }

            _noise = EditorGUILayout.Foldout(_noise, "NoiseData Values");
            if (_noise)
            {
                _mapController.NoiseData.NoiseScale = EditorGUILayout.FloatField("Noise Scale",
                    _mapController.NoiseData.NoiseScale);
                _mapController.NoiseData.Octaves = EditorGUILayout.IntField("Octaves Numbers",
                    _mapController.NoiseData.Octaves);
                _mapController.NoiseData.Persistence = EditorGUILayout.Slider("Persistence Value",
                    _mapController.NoiseData.Persistence, 0,
                    1);
                _mapController.NoiseData.Lacunarity = EditorGUILayout.FloatField("Lacunarity Value",
                    _mapController.NoiseData.Lacunarity);
                _mapController.NoiseData.Offset = EditorGUILayout.Vector2Field("Octaves Noise Offset",
                    _mapController.NoiseData.Offset);
                _mapController.NoiseData.Seed = EditorGUILayout.IntField("Map Seed:", _mapController.NoiseData.Seed);
                if (GUILayout.Button("Generate New Seed"))
                {
                    _mapController.NoiseData.Seed = Ultility.GetRandomString(new Random(), 40).GetHashCode();
                }
            }
        }
        else {
            base.OnInspectorGUI();
        }

        if ( _autoUpdate ) {
            _mapController.GenerateMap();
        }

    }
}