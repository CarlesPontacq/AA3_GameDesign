using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] private float destroyTime;
    [SerializeField] private float speed = 6f;

    [Header("Collision")]
    private const string objectiveTag = "Enemy";
    private const string stationTag = "Station";
    private const string playerTag = "Player";
    
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.position += (Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == playerTag) return;

        switch (other.tag)
        {
            case objectiveTag:
                Debug.Log(objectiveTag);
                break;

            case stationTag:
                Debug.Log(stationTag);
                break;

            default:
                break;
        }
        
        Destroy(gameObject);
    }
}
