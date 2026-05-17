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

    private GameObject[] blocks;

    
}