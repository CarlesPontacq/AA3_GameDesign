using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] private float destroyTime;
    [SerializeField] private float speed = 6f;

    [Header("Cure Settings")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CureType cureType;
    [SerializeField] private Color cure1Color = Color.white;
    [SerializeField] private Color cure2Color = Color.red;
    [SerializeField] private Color cure3Color = Color.green;
    [SerializeField] private Color cure4Color = Color.blue;

    [Header("Collision")]
    private const string playerTag = "Player";
    private const string changeCureArea = "ChangeCureArea";
    
    void Start()
    {
        Destroy(gameObject, destroyTime);
        SetupAppearance();
    }

    void SetupAppearance()
    {
        switch (cureType)
        {
            case CureType.CURE1:
                spriteRenderer.color = cure1Color;
                break;
            case CureType.CURE2:
                spriteRenderer.color = cure2Color;
                break;
            case CureType.CURE3:
                spriteRenderer.color = cure3Color;
                break;
            case CureType.CURE4:
                spriteRenderer.color = cure4Color;
                break;
        }
    }

    public void SetCureType(CureType type)
    {
        cureType = type;
        SetupAppearance();
    }

    void Update()
    {
        transform.position += (Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == playerTag || other.tag == changeCureArea) return;
        
        Destroy(gameObject);
    }
}
