using System;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float stepDistance;
    [SerializeField] GameObject deathEffectPrefab;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;

    Vector2 movementVector;
    bool useSprite1 = true;
    public int Column { get; private set; }

    public event Action<EnemyMovement> OnDeath;

    public void Initialize(float stepDist, DirectionUtils.Direction dir)
    {
        stepDistance = stepDist;
        movementVector = DirectionUtils.ToVector2(dir);

        useSprite1 = true;
        spriteRenderer.sprite = sprite1;
    }

    public void Step()
    {
        transform.position += (Vector3)(movementVector * stepDistance);

        useSprite1 = !useSprite1;
        spriteRenderer.sprite = useSprite1 ? sprite1 : sprite2;
    }

    public void SetColumn(int column)
    {
        Column = column;
    }

    public void SetDirection(DirectionUtils.Direction dir)
    {
        movementVector = DirectionUtils.ToVector2(dir);
    }

    public void Die()
    {
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Die();
            Destroy(other.gameObject);
        }
    }
}