using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour 
{
    public float scale = 7.0f;
    public float heightScale = 1.0f;

	// Update is called once per frame
	void Update () 
    {
        var mf = GetComponent<MeshFilter>();
        var vertices = mf.mesh.vertices;

        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = heightScale * Mathf.PerlinNoise(Time.time + (vertices[i].x * scale), Time.time + (vertices[i].z * scale));
        }
        mf.mesh.vertices = vertices;
	}
}
