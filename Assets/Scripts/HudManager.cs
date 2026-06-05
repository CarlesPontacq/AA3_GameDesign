using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class HudManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI healthText;

    private void Update()
    {
        string formattedScore = FormatScoreWithMinimumDigits((int)ScoreManager.Instance.GetScore());

        if (scoreText.text != formattedScore)
        {
            SetScore((int)ScoreManager.Instance.GetScore());
        }

        string formattedHighScore = FormatScoreWithMinimumDigits((int)ScoreManager.Instance.GetHighScore());
        if (highScoreText.text != formattedHighScore)
        {
            SetHighScoreText((int)ScoreManager.Instance.GetHighScore());
        }
    }

    public void SetScore(int value)
    {
        scoreText.text = FormatScoreWithMinimumDigits(value);
    }

    public void SetHighScoreText(int value)
    {
        highScoreText.text = FormatScoreWithMinimumDigits(value);
    }

    public void SetHealth(int value)
    {
        healthText.text = value.ToString();
    }

    private string FormatScoreWithMinimumDigits(int score)
    {
        if (score < 1000)
        {
            return score.ToString("D4"); 
        }
        else
        {
            return score.ToString();
        }
    }
}
