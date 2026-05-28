using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private HudManager hudManager;

    [SerializeField] private int startingLives;

    private Camera camera;
    [SerializeField] private float targetWidth = 4f;
    [SerializeField] private float targetHeight = 5f;

    private int lives;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AdjustAspectRatio();
        lives = startingLives;

        hudManager = FindAnyObjectByType<HudManager>();
        hudManager.SetHealth(lives);
    }

    void Update()
    {

    }

    private void PlayerDied()
    {
        // Reinicio provisional
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReduceLives()
    {
        lives--;
        hudManager.SetHealth(lives);

        if (lives <= 0)
        {
            PlayerDied();
        }
    }

    void AdjustAspectRatio()
    {
        float targetAspect = targetWidth / targetHeight;
        float windowAspect = (float)Screen.width / (float)Screen.height;

        float scaleHeight = windowAspect / targetAspect;

        camera = FindAnyObjectByType<Camera>();

        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1f;
            rect.height = scaleHeight;
            rect.x = 0f;
            rect.y = (1f - scaleHeight) / 2f;

            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1f - scaleWidth) / 2f;
            rect.y = 0f;

            camera.rect = rect;
        }
    }
}
