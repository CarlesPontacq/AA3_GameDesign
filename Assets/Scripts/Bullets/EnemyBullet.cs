using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Collision")]
    private const string station = "Station";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == station)
        {
            Destroy(gameObject);
        };
    }
}
