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
    [SerializeField] float hitStopDuration = 0.05f;

    [Header("Spawn")]
    [SerializeField] float spawnDelay = 0.1f;

    [Header("Movement")]
    [SerializeField] float delayBetweenEnemies = 0.05f;
    [SerializeField] float dropDistance = 1f;

    List<EnemyMovement> enemies = new List<EnemyMovement>();

    bool justChangedDirection = false;
    
    Coroutine hitStopCoroutine;

    void Start()
    {
        StartCoroutine(GameRoutine());
    }

    IEnumerator GameRoutine()
    {
        yield return StartCoroutine(SpawnRoutine());
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

                Vector2 pos = origin + new Vector2(x * cellSize, y * cellSize);

                GameObject obj = Instantiate(type.prefab, pos, Quaternion.identity);

                var enemy = obj.GetComponent<EnemyMovement>();
                if (enemy != null)
                {
                    enemy.Initialize(stepDistance, initialDirection);

                    enemy.OnDeath += HandleEnemyDeath;

                    enemies.Add(enemy);
                }

                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }

    IEnumerator MovementRoutine()
    {
        while (true)
        {
            if (enemies.Count == 0)
            {
                yield return null;
                continue;
            }

            if (IsAtScreenEdge(out var newDir))
            {
                if (!justChangedDirection)
                {
                    SetDirectionAll(newDir);
                    MoveDown(dropDistance);
                    justChangedDirection = true;
                }
            }
            else
            {
                justChangedDirection = false;
            }

            var currentWave = new List<EnemyMovement>(enemies);

            for (int i = 0; i < currentWave.Count; i++)
            {
                var enemy = currentWave[i];
                if (enemy == null) continue;

                enemy.Step();

                yield return new WaitForSeconds(delayBetweenEnemies);
            }
        }
    }

    void HandleEnemyDeath(EnemyMovement enemy)
    {
        enemies.Remove(enemy);

        if (hitStopCoroutine != null)
            StopCoroutine(hitStopCoroutine);

        hitStopCoroutine = StartCoroutine(HitStop());
    }

    IEnumerator HitStop()
    {
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(hitStopDuration);

        Time.timeScale = 1f;
    }

    bool IsAtScreenEdge(out DirectionUtils.Direction newDirection)
    {
        newDirection = DirectionUtils.Direction.Right;

        float leftMost = float.MaxValue;
        float rightMost = float.MinValue;

        foreach (var enemy in enemies)
        {
            float x = enemy.transform.position.x;

            if (x < leftMost) leftMost = x;
            if (x > rightMost) rightMost = x;
        }

        float screenLeft = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        float screenRight = Camera.main.ViewportToWorldPoint(Vector3.right).x;

        if (rightMost >= screenRight)
        {
            newDirection = DirectionUtils.Direction.Left;
            return true;
        }

        if (leftMost <= screenLeft)
        {
            newDirection = DirectionUtils.Direction.Right;
            return true;
        }

        return false;
    }

    void MoveDown(float amount)
    {
        foreach (var enemy in enemies)
        {
            enemy.transform.position += Vector3.down * amount;
        }
    }

    void SetDirectionAll(DirectionUtils.Direction dir)
    {
        foreach (var enemy in enemies)
        {
            enemy.SetDirection(dir);
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