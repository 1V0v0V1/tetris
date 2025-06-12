using UnityEngine;

public class GridLineGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 20;
    public float cellSize = 0.5f;


    public Material lineMaterial;

    void Start()
    {
        float offsetX = -width * cellSize / 2f;
        float offsetY = -height * cellSize / 2f;

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

}
