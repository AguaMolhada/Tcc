using UnityEngine;

[CreateAssetMenu(menuName = "Data/Noise")]
public class NoiseData : UpdatableObject {
    public float NoiseScale;

    public int Octaves;
    [Range(0,1)]
    public float Persistence;
    public float Lacunarity;

    public int Seed;
    public Vector2 Offset;

    protected override void OnValidate()
    {
        if (Lacunarity < 0)
        {
            Lacunarity = 1;
        }
        if (Octaves < 0)
        {
            Octaves = 1;
        }
        base.OnValidate();
    }
}
