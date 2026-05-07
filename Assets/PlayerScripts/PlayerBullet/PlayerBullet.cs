using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("Basic Sttings")]
    [SerializeField] private float destroyTime;
    [SerializeField] private float speed = 6f;

    [Header("Collision")]
    [SerializeField] private string objectiveTag = "Enemy";
    
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
        //Comprobación de si tiene el componente para quitarle vida o ver si es el tipo de medicina que se necesita
        if (other.tag == objectiveTag)
        {
            Destroy(gameObject);
        }
    }
}
