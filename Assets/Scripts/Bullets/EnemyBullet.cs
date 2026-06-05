using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Collision")]
    private const string station = "Station";
    private const string playerBullet = "PlayerBullet";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == station || other.tag == playerBullet)
        {
            Destroy(gameObject);
        };
    }
}
