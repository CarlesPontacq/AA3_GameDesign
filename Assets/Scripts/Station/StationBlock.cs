using UnityEngine;

public class StationBlock : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet") || other.CompareTag("EnemyBullet"))
        {
            StationDestroyer bunker = GetComponentInParent<StationDestroyer>();
            if (bunker != null)
            {
                bunker.BlockDestroyed(transform);
            }

            Destroy(other);
        }
    }
}
