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
        if(scoreText.text != ScoreManager.Instance.GetScore().ToString())
        {
            SetScore((int)ScoreManager.Instance.GetScore());
        }

        if (highScoreText.text != ScoreManager.Instance.GetHighScore().ToString())
        {
            SetHighScoreText((int)ScoreManager.Instance.GetHighScore());
        }
    }

    public void SetScore(int value)
    {
        scoreText.text = value.ToString();
    }

    public void SetHighScoreText(int value)
    {
        highScoreText.text = value.ToString();
    }

    public void SetHealth(int value)
    {
        healthText.text = value.ToString();
    }
}
