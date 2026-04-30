using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Formation")]
    [SerializeField] FormationFromTilemap formation;
    [SerializeField] Vector2 startPosition;
    [SerializeField] float cellSize = 1.5f;

    [Header("Enemy Settings")]
    [SerializeField] float stepDistance = 1f;
    [SerializeField] float timeBetweenSteps = 1f;
    [SerializeField] DirectionUtils.Direction initialDirection = DirectionUtils.Direction.Right;

    List<EnemyMovement> enemies = new List<EnemyMovement>();

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        float dt = Time.deltaTime;
        UpdateEnemies(dt);
    }

    void Spawn()
    {
        enemies.Clear();

        Vector2 origin = GetCenteredOrigin();

        for (int y = 0; y < formation.height; y++)
        {
            for (int x = 0; x < formation.width; x++)
            {
                var type = formation.grid[x + y * formation.width];
                if (type == null) continue;

                Vector2 pos =
                    origin +
                    new Vector2(x * cellSize, y * cellSize);

                GameObject obj = Instantiate(type.prefab, pos, Quaternion.identity);

                var enemy = obj.GetComponent<EnemyMovement>();
                if (enemy != null)
                {
                    enemy.Initialize(stepDistance, timeBetweenSteps, initialDirection);
                    enemies.Add(enemy);
                }
            }
        }
    }

    void UpdateEnemies(float dt)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
                enemies[i].Tick(dt);
        }
    }

    Vector2 GetCenteredOrigin()
    {
        return startPosition -
            new Vector2(
                (formation.width - 1) * cellSize * 0.5f,
                (formation.height - 1) * cellSize * 0.5f
            );
    }
}