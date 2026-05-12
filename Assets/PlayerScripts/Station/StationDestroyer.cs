using UnityEngine;

public class StationDestroyer : MonoBehaviour
{
    [Header("Station Size")]
    public int width = 7;
    public int height = 5;     

    [Header("Prefabs")]
    public GameObject blockPrefab;

    [Header("Blocks")]
    public float blockSize = 0.11f;

    [Header("Destruction")]
    public int destroyRadius = 2;

    private GameObject[,] blocks;

    void Start()
    {
        GenerateBunker();
    }

    void GenerateBunker()
    {
        blocks = new GameObject[width, height];

        int[,] shape = GetBunkerShape();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (shape[x, y] == 1)
                {
                    Vector3 blockPos = transform.position + new Vector3(
                        (x - (width - 1) / 2f) * blockSize,
                        y * blockSize,
                        0
                    );

                    GameObject block = Instantiate(blockPrefab, blockPos, Quaternion.identity);
                    block.transform.parent = transform;

                    block.transform.localScale = Vector3.one * blockSize;

                    blocks[x, y] = block;
                }
            }
        }
    }

    int[,] GetBunkerShape()
    {
        int[,] shape = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float nx = (x - (width - 1) / 2f) / (width / 2f);
                float ny = y / (float)height;

                float shapeHeight = 1 - (nx * nx) * 1.2f;

                if (ny < shapeHeight && ny > 0.1f)
                {
                    shape[x, y] = 1;
                }
                else
                {
                    shape[x, y] = 0;
                }
            }
        }

        return shape;
    }

    public void BlockDestroyed(Transform destroyedBlock)
    {
        Vector2Int coords = GetBlockCoordinates(destroyedBlock);

        if (coords.x == -1) return;

        for (int x = -destroyRadius; x <= destroyRadius; x++)
        {
            for (int y = -destroyRadius; y <= destroyRadius; y++)
            {
                int targetX = coords.x + x;
                int targetY = coords.y + y;

                if (targetX >= 0 && targetX < width && targetY >= 0 && targetY < height)
                {
                    if (blocks[targetX, targetY] != null)
                    {
                        Destroy(blocks[targetX, targetY]);
                        blocks[targetX, targetY] = null;
                    }
                }
            }
        }
    }

    Vector2Int GetBlockCoordinates(Transform block)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (blocks[x, y] != null && blocks[x, y].transform == block)
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        float totalWidth = width * blockSize;
        float totalHeight = height * blockSize;
        Vector3 center = transform.position + new Vector3(0, totalHeight / 2f, 0);
        Gizmos.DrawWireCube(center, new Vector3(totalWidth, totalHeight, 0));
    }
}