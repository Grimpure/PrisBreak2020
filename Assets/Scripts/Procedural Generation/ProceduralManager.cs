using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProceduralManager : MonoBehaviour
{
    #region Singleton

    private static ProceduralManager instance;

    public static ProceduralManager Instance
    {
        get { return instance; }
        set
        {
            if (instance == null)
            {
                instance = value;
            }
        }
    }
    private void Awake()
    {
        Instance = this;
    
        world.Init();
    }

    //private void Awake()
    //{
    //    if(instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(this);
    //    }
    //}

    #endregion

    private int seed = 19834742;
    private float perlinSeed;
    public ProceduralWorld world;

    public delegate void OnGenerate();
    public static OnGenerate regenerate;

    private void OnValidate()
    {
        if (Instance != null)
        {
            regenerate.Invoke();
            Debug.Log("OnValidate is Called!");
        }
    }

    public void SetSeed(int seed)
    {
        this.seed = seed;
        Random.InitState(seed);
        perlinSeed = Random.Range(-100000.0f, 100000.0f);
    }

    public float GetPerlinSeed()
    {
        return perlinSeed;
    }
}
