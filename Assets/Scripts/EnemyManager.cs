using System.Collections;
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
    [SerializeField] DirectionUtils.Direction initialDirection = DirectionUtils.Direction.Right;

    [Header("Spawn")]
    [SerializeField] float spawnDelay = 0.1f;

    [Header("Movement")]
    [SerializeField] float delayBetweenEnemies = 0.05f;

    List<EnemyMovement> enemies = new List<EnemyMovement>();

    void Start()
    {
        StartCoroutine(GameRoutine());
    }

    IEnumerator GameRoutine()
    {
        yield return StartCoroutine(SpawnRoutine());

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(MovementRoutine());
    }

    IEnumerator SpawnRoutine()
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
                    enemy.Initialize(stepDistance, initialDirection);
                    enemies.Add(enemy);
                }

                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }

    IEnumerator MovementRoutine()
    {
        int index = 0;

        while (true)
        {
            if (enemies.Count == 0)
            {
                yield return null;
                continue;
            }

            if (enemies[index] != null)
            {
                enemies[index].Step();
            }

            index++;
            if (index >= enemies.Count)
                index = 0;

            yield return new WaitForSeconds(delayBetweenEnemies);
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