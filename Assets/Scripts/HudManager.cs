using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class HudManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI healthText;

    public void SetScore(int value)
    {
        scoreText.text = value.ToString();
    }

    public void SetHealth(int value)
    {
        healthText.text = value.ToString();
    }
}
