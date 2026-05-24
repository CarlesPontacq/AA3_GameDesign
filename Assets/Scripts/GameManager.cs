using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private HudManager hudManager;

    [SerializeField] private int startingLives;

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
        lives = startingLives;
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

}
