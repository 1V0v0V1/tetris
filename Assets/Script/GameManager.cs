using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int width = 10;
    public static int height = 20;
    public static Transform[,] grid = new Transform[width, height];

    public GameObject tetrominoPrefab;

    void Start()
    {
        Instantiate(tetrominoPrefab, new Vector3(5, 18, 0), Quaternion.identity);
        DrawGridLines();  // ← 追加
    }

    void DrawGridLines()
    {
        GameObject gridParent = new GameObject("GridLines");

        // 縦線
        for (int x = 0; x <= width; x++)
        {
            CreateLine(new Vector3(x, 0, 0), new Vector3(x, height, 0), gridParent.transform);
        }

        // 横線
        for (int y = 0; y <= height; y++)
        {
            CreateLine(new Vector3(0, y, 0), new Vector3(width, y, 0), gridParent.transform);
        }
    }

    void CreateLine(Vector3 start, Vector3 end, Transform parent)
    {
        GameObject line = new GameObject("Line");
        line.transform.parent = parent;
        LineRenderer lr = line.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.material = new Material(Shader.Find("Sprites/Default")); // 必須
        lr.startColor = Color.gray;
        lr.endColor = Color.gray;
        lr.sortingOrder = -1; // 背景に
    }

    public static Vector2 RoundVector(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool InsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
    }
}
