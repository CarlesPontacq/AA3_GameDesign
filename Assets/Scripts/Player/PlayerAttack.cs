using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;

    [Header("Attack")]
    [SerializeField] private float fireRate = 0.3f;

    [Header("Input")]
    [SerializeField] private PlayerInputObserver input;

    private float timer;
    private bool canAttack = false;

    private void Start()
    {
        input.onAttack += Shoot;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate) { 
            timer = 0;
            canAttack = true;
        }
    }

    private void Shoot()
    {
        if(canAttack)
        {
            Instantiate(projectilePrefab, shootPoint.transform.position, shootPoint.transform.rotation);
            canAttack = false;
        }
    }
}
