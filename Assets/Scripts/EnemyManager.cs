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

    [Header("Attack")]
    [SerializeField] GameObject enemyBulletPrefab;
    [SerializeField] float attackRate = 1f;

    float attackTimer;

    List<Enemy> enemies = new List<Enemy>();
    
    Coroutine hitStopCoroutine;

    void Start()
    {
        StartCoroutine(GameRoutine());
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackRate)
        {
            attackTimer = 0f;
            Shoot();
        }

        if(enemies.Count <= 0)
        {
            //SceneController.Instance.LoadNextScene();
        }
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
                var enemy = obj.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.Initialize(stepDistance, initialDirection);
                    
                    enemy.SetColumn(x);

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

            var currentWave = new List<Enemy>(enemies);

            for (int i = 0; i < currentWave.Count; i++)
            {
                var enemy = currentWave[i];
                if (enemy == null) continue;

                enemy.Step();

                yield return new WaitForSeconds(delayBetweenEnemies);
            }

            if (IsAtScreenEdge(out var newDir))
            {
                yield return StartCoroutine(MoveDiagonalRoutine(newDir, dropDistance));
                SetDirectionAll(newDir);
            }
        }
    }

    void HandleEnemyDeath(Enemy enemy)
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
            var bounds = enemy.GetComponent<SpriteRenderer>().bounds;

            float left = bounds.min.x;
            float right = bounds.max.x;

            if (left < leftMost) leftMost = left;
            if (right > rightMost) rightMost = right;
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

    IEnumerator MoveDiagonalRoutine(DirectionUtils.Direction dir, float downAmount)
    {
        Vector2 horizontal = DirectionUtils.ToVector2(dir) * stepDistance;
        Vector2 vertical = Vector2.down * downAmount;
        Vector2 total = horizontal + vertical;

        var currentWave = new List<Enemy>(enemies);

        for (int i = 0; i < currentWave.Count; i++)
        {
            var enemy = currentWave[i];
            if (enemy == null) continue;

            enemy.transform.position += (Vector3)total;

            yield return new WaitForSeconds(delayBetweenEnemies);
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

    void Shoot()
    {
        Enemy shooter = GetBottomEnemy();

        if (shooter == null)
            return;

        shooter.Shoot(enemyBulletPrefab);
    }

    Enemy GetBottomEnemy()
    {
        Dictionary<int, Enemy> bottomEnemies =
            new Dictionary<int, Enemy>();

        foreach (var enemy in enemies)
        {
            if (enemy == null)
                continue;

            int column = enemy.Column;

            if (!bottomEnemies.ContainsKey(column))
            {
                bottomEnemies[column] = enemy;
                continue;
            }

            if (enemy.transform.position.y <
                bottomEnemies[column].transform.position.y)
            {
                bottomEnemies[column] = enemy;
            }
        }

        if (bottomEnemies.Count == 0)
            return null;

        List<Enemy> candidates =
            new List<Enemy>(bottomEnemies.Values);

        return candidates[Random.Range(0, candidates.Count)];
    }
}