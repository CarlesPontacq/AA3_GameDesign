using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private float score;
    [SerializeField] private float highScore;
    [SerializeField] private float addScore = 100;
    [SerializeField] private float bonusModifier = 0.1f;
    [SerializeField] private float bonus = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", highScore);
    }

    void Update()
    {
        if(score > highScore)
        {
            highScore = score;
        }
    }

    public void AddScore()
    {
        score = score + (addScore + (addScore * bonusModifier * bonus));
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void CountBonus()
    {
        bonus++;
    }

    public void ResetBonus()
    {
        bonus = 0;
    }

    public float GetScore()
    {
        return score;
    }

    public float GetHighScore()
    {
        return highScore;
    }
}
