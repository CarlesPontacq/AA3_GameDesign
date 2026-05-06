using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    [SerializeField] float duration;

    private void Start()
    {
        Destroy(gameObject, duration);
    }
}
