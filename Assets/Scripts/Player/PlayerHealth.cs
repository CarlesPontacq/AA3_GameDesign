using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 2;
    [SerializeField] private List<Animator> animators;
    [SerializeField] private SpriteRenderer exteriorSpriteRenderer;
    [SerializeField] private SpriteRenderer interiorSpriteRenderer;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerAttack attack;

    private Vector2 spawnPoint;

    private const string enemyBulletTag = "EnemyBullet";

    private bool isDying = false;

    private void Start()
    {
        spawnPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDying) return;

        if (other.CompareTag(enemyBulletTag))
        {
            Destroy(other.gameObject);
            StartCoroutine(DieCoroutine());
        }
    }

    private IEnumerator DieCoroutine()
    {
        isDying = true;

        EnablePlayerControl(false);

        foreach (Animator animator in animators)
        {
            if (animator != null)
                animator.SetTrigger("Die");
        }

        if (animators.Count > 0 && animators[0] != null)
        {
            yield return null;

            AnimatorStateInfo stateInfo = animators[0].GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(stateInfo.length);
        }

        EnablePlayerSprites(false);

        yield return new WaitForSeconds(respawnDelay);

        transform.position = spawnPoint;

        EnablePlayerSprites(true);
        EnablePlayerControl(true);

        GameManager.Instance.ReduceLives();

        isDying = false;
    }

    void EnablePlayerControl(bool enable)
    {
        circleCollider.enabled = enable;
        movement.enabled = enable;
        attack.enabled = enable;
    }

    void EnablePlayerSprites(bool enable)
    {
        exteriorSpriteRenderer.enabled = enable;
        interiorSpriteRenderer.enabled = enable;
    }
}