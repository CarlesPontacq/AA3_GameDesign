using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] CureType weakness;

    [SerializeField] float stepDistance;
    [SerializeField] GameObject deathEffectPrefab;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;

    [SerializeField] Transform shootPoint;

    Vector2 movementVector;
    bool useSprite1 = true;
    public int Column { get; private set; }

    public event Action<Enemy> OnDeath;

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

    public void Shoot(GameObject bullet)
    {
        Instantiate(
            bullet,
            shootPoint.transform.position,
            Quaternion.identity
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            CureType cureType = other.GetComponent<PlayerBullet>().GetCureType();
            Debug.Log("Enemigo con weakness " + weakness + " recibe medicina " + cureType);
            if (weakness == CureType.ANY || weakness == cureType)
            {
                SFXManager.Instance.PlayGlobalSound("EnemyHit", 1f);

                Die();
                Destroy(other.gameObject);
            }
            else
            {
                SFXManager.Instance.PlayGlobalSound("WrongBullet", 1f);
            }
        }
    }
}