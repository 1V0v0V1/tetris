using UnityEngine;

public class TetrominoSpawner : MonoBehaviour
{
    public GameObject tetrominoPrefab;

    private GameObject currentTetromino;
    private float fallTime = 1f; //落ちる速度
    private float fallTimer = 0f; //これを使ってフレームを計測。これが一定値を超えるとブロックが落ちる。下でリセットしてるけど、リセットしなければブロックは落ち続ける。
    private float cellSize = 1f; //ブロックが落ちる間隔
    private const float floorY = -9.5f; //床の一番下のY軸の座標


    void Start()
    {
        SpawnNewTetromino();
    }

    void Update()
    {
        Debug.Log("Update実行中");

        if (currentTetromino == null) return;

        HandleInput();

        fallTimer += Time.deltaTime; //ここで時間増やす
        if (fallTimer >= fallTime)
        {
            if (IsLanded())
            {
                GridManager.AddToGrid(currentTetromino, cellSize);
                currentTetromino = null;
                SpawnNewTetromino();
            }
            else
            {
                Move(Vector3.down);
            }
            fallTimer = 0f;
        }
        
    }


    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) // ← 押しっぱなしだと早すぎるので GetKeyDown に修正
        {
            Move(Vector3.down);
            if (IsLanded()) // 追加：キーによる移動でも着地判定
            {
                GridManager.AddToGrid(currentTetromino, cellSize);
                currentTetromino = null;
                SpawnNewTetromino();
            }
        }
    }


    void Move(Vector3 direction)
    {
        foreach (Transform block in currentTetromino.transform)
        {
            Vector3 nextPos = block.position + direction * cellSize;
            Vector2Int cell = GridManager.WorldToGrid(nextPos, cellSize);

            if (cell.x < 0 || cell.x >= GridManager.width || cell.y < 0 || cell.y >= GridManager.height || GridManager.IsCellOccupied(nextPos, cellSize))
            {
                Debug.Log("壁または他ブロックに衝突");
                return;
            }
        }

        // すべてのブロックが移動可能ならまとめて動かす
        currentTetromino.transform.position += direction * cellSize;
    }



    void SpawnNewTetromino()
    {
        currentTetromino = Instantiate(tetrominoPrefab, new Vector3(0.5f, 9.5f, 0f), Quaternion.identity);
    }


    bool IsLanded()
    {
        foreach (Transform block in currentTetromino.transform)
        {
            Vector3 nextPos = block.position + Vector3.down * cellSize;

            if (nextPos.y < floorY)
            {
                Debug.Log("床に達した！");
                return true;
            }

            if (GridManager.IsCellOccupied(nextPos, cellSize))
            {
                Debug.Log("他ブロックに接触！");
                return true;
            }
        }

        return false;
    }

}
