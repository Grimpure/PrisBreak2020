using UnityEngine;

public class TerrainScape : Landscape
{
    private Terrain t;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Terrain>();

        if (t == null)
        {
            Debug.LogError("Please put the TerrainScape Script on a Terrain!");
        }

        Init();
    }

    public override void Clean()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Rock");
        for (int i = 0; i < go.Length; i++)
        {
        Destroy(go[i]);
        }
    }

    public override void Generate()
    {
        Debug.Log("Adding Heights...");
        t.terrainData.heightmapResolution = ProceduralManager.Instance.world.Size;
        t.terrainData.SetHeights(0, 0, ProceduralManager.Instance.world.heights);

        for (int r = 0; r < ProceduralManager.Instance.world.rocks.Count; r++)
        {
            Vector3Int rock = ProceduralManager.Instance.world.rocks[r];
            float height = ProceduralManager.Instance.world.heights[rock.x, rock.y];

            Vector3 worldPosition = new Vector3
                (
                MathUtils.Map (
                    rock.x,
                    0,
                    ProceduralManager.Instance.world.Size,
                    t.GetPosition().x,
                    t.GetPosition().x + t.terrainData.size.x ),
                0.0f,
                MathUtils.Map (
                    rock.y,
                    0,
                    ProceduralManager.Instance.world.Size,
                    t.GetPosition().z,
                    t.GetPosition().z+t.terrainData.size.z )
                );
            worldPosition.y = t.SampleHeight(worldPosition);

            Instantiate(ProceduralManager.Instance.world.rockPrefabs[rock.z], worldPosition, Quaternion.identity);
        }
    }
}
