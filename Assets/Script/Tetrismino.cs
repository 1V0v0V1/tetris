using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public float fallSpeed = 1f;
    private float fallTimer = 0f;

    void Update()
    {
        HandleInput();
        HandleFall();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            transform.position += Vector3.left;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            transform.position += Vector3.right;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            transform.Rotate(0, 0, 90);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            transform.position += Vector3.down;
    }

    void HandleFall()
    {
        fallTimer += Time.deltaTime;
        if (fallTimer >= fallSpeed)
        {
            transform.position += Vector3.down;
            fallTimer = 0f;
        }
    }

    void AddToGrid()
    {
        foreach (Transform block in transform)
        {
            Vector2 pos = GameManager.RoundVector(block.position);
            GameManager.grid[(int)pos.x, (int)pos.y] = block;
        }
    }
}
