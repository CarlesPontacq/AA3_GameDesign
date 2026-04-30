using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float stepDistance;
    [SerializeField] float timeBetweenSteps;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;

    float stepTimer;
    Vector2 movementVector;
    bool useSprite1 = true;

    public void Initialize(float stepDist, float timeSteps, DirectionUtils.Direction dir)
    {
        stepDistance = stepDist;
        timeBetweenSteps = timeSteps;
        stepTimer = timeBetweenSteps;

        movementVector = DirectionUtils.ToVector2(dir);

        useSprite1 = true;
        spriteRenderer.sprite = sprite1;
    }

    void Move()
    {
        transform.position += (Vector3)(movementVector * stepDistance);

        useSprite1 = !useSprite1;
        spriteRenderer.sprite = useSprite1 ? sprite1 : sprite2;
    }

    public void Tick(float dt)
    {
        stepTimer -= dt;

        if (stepTimer <= 0f)
        {
            Move();
            stepTimer = timeBetweenSteps;
        }
    }

    public void SetDirection(DirectionUtils.Direction dir)
    {
        movementVector = DirectionUtils.ToVector2(dir);
    }
}