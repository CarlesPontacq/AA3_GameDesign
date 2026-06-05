using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private CureType currentCureType;

    [Header("Player Settings")]
    [SerializeField] private float fireRate = 0.3f;
    [SerializeField] private SpriteRenderer innerPlayerSprite;
    [SerializeField] private CureTypesClass cureTypeColors;

    [Header("Input")]
    [SerializeField] private PlayerInputObserver input;


    private float timer;
    private bool canAttack = false;
    private CureSwitcherArea cureSwitcherArea;

    private void Start()
    {
        input.onAttack += Shoot;
        input.onChangeCure += OnChangeCureInput;

        innerPlayerSprite.color = cureTypeColors.SetUpPlayerColors(currentCureType);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate) { 
            timer = 0;
            canAttack = true;
        }
    }

    private void OnChangeCureInput()
    {
        if (cureSwitcherArea != null)
            cureSwitcherArea.TryChangeCure();

        innerPlayerSprite.color = cureTypeColors.SetUpPlayerColors(currentCureType);

        Debug.Log("Change cure key pressed" + currentCureType.ToString());
    }

    private void Shoot()
    {
        if(canAttack)
        {
            GameObject bullet = Instantiate(projectilePrefab, shootPoint.transform.position, shootPoint.transform.rotation);

            bullet.GetComponent<PlayerBullet>().SetCureType(currentCureType);

            canAttack = false;
        }
    }

    public void SetCurrentCureType(CureType newtCureType) { currentCureType = newtCureType; }
    public void SetCureSwitchArea(CureSwitcherArea newCureSwitchArea) { cureSwitcherArea = newCureSwitchArea; }
    public void SetInnerSpriteColor(Color newColor) { innerPlayerSprite.color = newColor; }
}
