using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static int width = 10;
    public static int height = 20;
    private static float cellSize = 1f;
    public static float offsetX;
    public static float offsetY;

    public Material lineMaterial;

    public static Transform[,] grid;

    void Start()
    {
        grid = new Transform[width, height];

        offsetX = -width * cellSize / 2f;
        offsetY = -height * cellSize / 2f;

        // 横線
        for (int y = 0; y <= height; y++)
        {
            CreateLine(
                new Vector3(0 + offsetX, y * cellSize + offsetY, 0),
                new Vector3(width * cellSize + offsetX, y * cellSize + offsetY, 0)
            );
        }

        // 縦線
        for (int x = 0; x <= width; x++)
        {
            CreateLine(
                new Vector3(x * cellSize + offsetX, 0 + offsetY, 0),
                new Vector3(x * cellSize + offsetX, height * cellSize + offsetY, 0)
            );
        }
    }

    void CreateLine(Vector3 start, Vector3 end)
    {
        GameObject lineObj = new GameObject("Line");
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();

        lr.material = lineMaterial;
        lr.startColor = Color.green;
        lr.endColor = Color.green;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.useWorldSpace = true;
        lr.textureMode = LineTextureMode.Tile;

        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    public static Vector2Int WorldToGrid(Vector3 worldPos, float cellSize)
    {
        float offsetX = -width * cellSize / 2f;
        float offsetY = -height * cellSize / 2f;

        int x = Mathf.FloorToInt((worldPos.x - offsetX) / cellSize);
        int y = Mathf.FloorToInt((worldPos.y - offsetY) / cellSize);

        return new Vector2Int(x, y);
    }


    public static bool IsCellOccupied(Vector3 worldPos, float cellSize)
    {
        Vector2Int cell = WorldToGrid(worldPos, cellSize);

        if (cell.x < 0 || cell.x >= width || cell.y < 0 || cell.y >= height)
            return true;

        return grid[cell.x, cell.y] != null;
    }

    public static void AddToGrid(GameObject tetromino, float cellSize)
    {
        foreach (Transform block in tetromino.transform)
        {
            Vector2Int cell = WorldToGrid(block.position, cellSize);

            if (cell.x >= 0 && cell.x < width && cell.y >= 0 && cell.y < height)
            {
                grid[cell.x, cell.y] = block;
            }
        }
    }
}
