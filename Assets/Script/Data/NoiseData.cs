using UnityEngine;

/// <summary>
/// Class used to store all the noise variables that will generate the map
/// </summary>
[CreateAssetMenu(menuName = "Data/Noise")]
public class NoiseData : UpdatableObject
{
    /// <summary>
    /// Zoom of the noise
    /// </summary>
    public float NoiseScale;

    /// <summary>
    /// Number of octaves used in the perlin function
    /// </summary>
    public int Octaves;
    /// <summary>
    /// Number that will "smooth" the perlin noise"
    /// </summary>
    [Range(0, 1)] public float Persistence;
    /// <summary>
    /// I dont know how to explain this good value is 2-4
    /// </summary>
    public float Lacunarity;
    /// <summary>
    /// Seed used to generate the random and the map
    /// </summary>
    public int Seed;
    /// <summary>
    /// Offset that the map will draw the noise
    /// </summary>
    public Vector2 Offset;

    /// <summary>
    /// Each time that some variable is changed will validate and if the value is below 0 will return to 1
    /// </summary>
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
