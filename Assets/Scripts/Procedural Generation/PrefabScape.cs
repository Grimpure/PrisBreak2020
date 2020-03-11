using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PrefabScape : Landscape
{
    public GameObject prefab;
    public float minHeight = 0.0f, maxHeight = 1.0f;

    public MeshFilter[] meshFilters;
    public CombineInstance[] combine;

    public bool isFloored;

    [Header("XYZ")]
    public int width = 16;
    public int height = 16;
    public int depth = 16;

    [Header("PerlinNoise")]
    public float freq = 50.0f; //Ideal on seed = 10 is 28 freq for the 2D frid and 6 for 3D
    public float amp = 30.0f; //Ideal on seed = 10 is 30 amp for the 2D grid and 1 for 3D

    [Space(5.0f)]
    public float detail = 10.0f;

    private void Start()
    {
        //Generate();
        //GenerateX_Z();
        //GenerateZ_Y_Z();
    }

    public override void Generate()
    {
        Clean();

        for (int x = 0; x < ProceduralManager.Instance.world.Size; x++)
        {
            for (int z = 0; z < ProceduralManager.Instance.world.Size; z++)
            {
                float y = ProceduralManager.Instance.world.heights[x, z];
                y = isFloored ? Mathf.Floor(y) : y;

                Vector3 pos = new Vector3(x, y, z);
                GameObject go = Instantiate(prefab, pos, Quaternion.identity, transform);
            }
        }
    }

    public override void Clean()
    {
        throw new System.NotImplementedException(); 
    }

    //private void PerlinNoise3D(float x, float y, float z)
    //{
    //    float xy = Mathf.PerlinNoise(x / freq, y / freq) * amp;
    //    float xz = Mathf.PerlinNoise(x / freq, z / freq) * amp;
    //    float yz = Mathf.PerlinNoise(y / freq, z / freq) * amp;
    //    float yx = Mathf.PerlinNoise(y / freq, x / freq) * amp;
    //    float zx = Mathf.PerlinNoise(z / freq, x / freq) * amp;
    //    float zy = Mathf.PerlinNoise(z / freq, y / freq) * amp;
    //
    //    float xyz = ((xy + xz + yz + yx + zx + zy) / 6.0f);
    //
    //    return xyz;
    //}
}
