using UnityEngine;

public abstract class Landscape : MonoBehaviour
{
    protected void Init()
    {
        ProceduralManager.Instance.SetSeed(10);
        ProceduralManager.regenerate += Generate;

        Generate();
    }

    public abstract void Clean();
    public abstract void Generate();
}
