using UnityEngine;
public class EnemyClickKillTest : MonoBehaviour
{
    Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnMouseDown()
    {
        if (enemy != null)
        {
            enemy.Die();
        }
    }
}