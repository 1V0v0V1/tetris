using UnityEngine;

public class TetrominoSpawner : MonoBehaviour
{
    public GameObject tetrominoPrefab;

    private GameObject currentTetromino;
    private float fallTime = 1f; //落ちる速度
    private float fallTimer = 0f; //これを使ってフレームを計測。これが一定値を超えるとブロックが落ちる。下でリセットしてるけど、リセットしなければブロックは落ち続ける。
    private float cellSize = 1f; //ブロックが落ちる間隔

    void Start()
    {
        SpawnNewTetromino();
    }

    void Update()
    {

        if (currentTetromino == null) return;

        HandleInput();

        fallTimer += Time.deltaTime; //ここで時間増やす
        if (fallTimer >= fallTime)
        {
            currentTetromino.transform.position += new Vector3(0, -cellSize, 0);
            fallTimer = 0f;
        }
    }

    void HandleInput() //操作のお話
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(Vector3.down); // 押しっぱなしで速く落ちる (ちょっと早すぎるから後で調整)
        }
    }

    void Move(Vector3 direction) //どれだけ移動するか
    {
        currentTetromino.transform.position += direction * cellSize;
    }

    void SpawnNewTetromino()
    {
        currentTetromino = Instantiate(tetrominoPrefab, new Vector3(0.5f, 9.5f, 0f), Quaternion.identity);
    }
}
