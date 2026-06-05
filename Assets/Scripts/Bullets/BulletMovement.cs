using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float destroyTime = 5f;
    [SerializeField] private DirectionUtils.Direction direction;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        Vector2 dir = DirectionUtils.ToVector2(direction);
        transform.position += (Vector3)(dir * speed * Time.deltaTime);
    }

    public void SetDirection(DirectionUtils.Direction newDirection)
    {
        direction = newDirection;
    }
}