using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject blockPrefab;

    void Start()
    {
        // 指定座標にブロックを表示（0, 0）座標
        Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}

