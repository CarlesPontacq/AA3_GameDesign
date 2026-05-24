using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const string enemyBulletTag = "EnemyBullet";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == enemyBulletTag)
        {
            GameManager.Instance.ReduceLives();
            Destroy(other.gameObject);
        };
    }
}
