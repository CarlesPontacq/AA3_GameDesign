using UnityEngine;
public class EnemyClickKillTest : MonoBehaviour
{
    EnemyMovement enemy;

    void Awake()
    {
        enemy = GetComponent<EnemyMovement>();
    }

    void OnMouseDown()
    {
        if (enemy != null)
        {
            enemy.Die();
        }
    }
}