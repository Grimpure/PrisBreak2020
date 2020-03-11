using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProceduralWorld
{
    public enum GenType
    {
        RandomBased,
        PerlinBased,
        SinBased
    };

    [SerializeField] public List<GameObject> rockPrefabs;

    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    [SerializeField] private int size;
    [SerializeField] private float detail;
    [SerializeField] private float rockProbability;
    [SerializeField] private int seed;
    [SerializeField] private GenType type;
    public float[,] heights;

    [SerializeField] public List<Vector3Int> rocks;

    public int Size
    {
        get { return size; }
        set
        {
            size = value;
            Init();
        }
    }

    public float Detail
    {
        get { return detail; }
        set
        {
            detail = value;
            Init();
        }
    }

    public float[,] Heights
    {
        get { return heights; }
        set
        {
            heights = value;
            Init();
        }
    }

    public ProceduralWorld(float minHeight, float maxHeight, int size, float detail, int seed, GenType type)
    {
        Debug.Log("Constructor of the world called");
        this.minHeight = minHeight;
        this.maxHeight = maxHeight;
        this.size = size;
        this.detail = detail;
        this.seed = seed;
        
    }

    public void Init()
    {
        ProceduralManager.regenerate += Regenerate;
        Debug.Log("Added Listener");
        Regenerate();
    }

    public void Regenerate()
    {
        heights = new float[size, size];
        ProceduralManager.Instance.SetSeed(seed);
        Generate();
    }

    public void Generate()
    {
        for (int x = 0; x < heights.GetLength(0); x++)
        {
            for (int z = 0; z < heights.GetLength(1); z++)
            {
                float height = 0;

                switch (type)
                {
                    case GenType.RandomBased:
                        height = UnityEngine.Random.Range(minHeight, maxHeight);
                        break;
                    case GenType.PerlinBased:
                        float perlinX = ProceduralManager.Instance.GetPerlinSeed() + x / (float)size * detail;
                        float perlinY = ProceduralManager.Instance.GetPerlinSeed() + z / (float)size * detail;
                        height = (Mathf.PerlinNoise(perlinX, perlinY) - minHeight) * maxHeight;
                        break;
                    case GenType.SinBased:
                        height = Mathf.Sin(x + z / detail) + Mathf.Cos(z / detail) * UnityEngine.Random.Range(minHeight, maxHeight);
                        break;
                }

                heights[x, z] = height;

                float rockRand = UnityEngine.Random.value;

                if (rockRand < rockProbability * (maxHeight/height))
                {
                    int t = UnityEngine.Random.Range(0, rockPrefabs.Count);
                    Vector3Int rock = new Vector3Int(x, z, t);
                    rocks.Add(rock);
                }
            }
        }
        Debug.Log("World Generated!");
    }
}
