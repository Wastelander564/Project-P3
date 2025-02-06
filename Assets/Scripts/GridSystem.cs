using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int width = 10, height = 10;
    public float cellSize = 1f;

    
    [HideInInspector] public float[,] lightingLevels; 

    private void Awake()
    {
       
        lightingLevels = new float[width, height];
    }

    private void Start()
    {
        
        GenerateLighting(); 
    }

    void GenerateLighting()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
               
                lightingLevels[x, y] = Random.Range(0.1f, 1f);
            }
        }
    }

    public float GetLightingLevel(Vector2 position)
    {
        int x = Mathf.FloorToInt(position.x / cellSize);
        int y = Mathf.FloorToInt(position.y / cellSize);

        if (x >= 0 && x < width && y >= 0 && y < height)
            return lightingLevels[x, y];
        else
            return 1f; 
    }

    private void OnDrawGizmos()
    {
        if (lightingLevels == null) return; 

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
               
                Gizmos.color = new Color(lightingLevels[x, y], lightingLevels[x, y], lightingLevels[x, y], 0.2f); 
                Gizmos.DrawWireCube(new Vector3(x * cellSize, y * cellSize, 0), new Vector3(cellSize, cellSize, 0));
            }
        }
    }
}
