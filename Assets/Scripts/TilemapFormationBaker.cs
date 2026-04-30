using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapFormationBaker : MonoBehaviour
{
    [Header("Source")]
    [SerializeField] Tilemap tilemap;

    [Header("Mapping")]
    [SerializeField] List<TileEnemyMapping> mappings = new();

    [Header("Output")]
    [SerializeField] FormationFromTilemap output;

    void OnEnable()
    {
        if (mappings == null)
            mappings = new List<TileEnemyMapping>();
    }

    [ContextMenu("Bake Formation")]
    public void Bake()
    {
        if (tilemap == null || output == null)
        {
            Debug.LogWarning("Tilemap o Output no asignados");
            return;
        }

        CleanMappings();

        BoundsInt bounds = tilemap.cellBounds;

        output.width = bounds.size.x;
        output.height = bounds.size.y;
        output.grid = new EnemyTypeData[output.width * output.height];

        foreach (var pos in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile == null) continue;

            EnemyTypeData type = GetTypeFromTile(tile);
            if (type == null) continue;

            Vector3Int local = new Vector3Int(
                pos.x - bounds.xMin,
                pos.y - bounds.yMin,
                0
            );

            int index = local.x + local.y * output.width;
            if (index >= 0 && index < output.grid.Length)
            {
                output.grid[index] = type;
            }
        }

        Debug.Log("Formation baked correctamente");
    }

    void CleanMappings()
    {
        mappings.RemoveAll(m => m == null || m.tile == null || m.enemyType == null);
    }

    EnemyTypeData GetTypeFromTile(TileBase tile)
    {
        for (int i = 0; i < mappings.Count; i++)
        {
            if (mappings[i].tile == tile)
                return mappings[i].enemyType;
        }
        return null;
    }
}