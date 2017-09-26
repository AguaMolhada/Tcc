using UnityEngine;

[CreateAssetMenu()]
public class TerrainData : UpdatableObject {

    public bool FlatShading;
    public bool UseFallout;

    public int MeshHeightMultiplier;
    public AnimationCurve MeshHeightCurve;

    public float MinHeight { get { return MeshHeightMultiplier * MeshHeightCurve.Evaluate(0); } }
    public float MaxHeight { get { return MeshHeightMultiplier * MeshHeightCurve.Evaluate(1); } }

}
