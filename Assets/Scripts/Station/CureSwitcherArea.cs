using System;
using TMPro;
using UnityEngine;

public class CureSwitcherArea : MonoBehaviour
{
    [SerializeField] private CureType stationCureType;
    [SerializeField] private CureTypesClass stationCureTypeClass;

    [SerializeField] private SpriteRenderer[] areaVisual;
    private Color activeColor = Color.white;
    private Color inactiveColor = Color.gray;

    [SerializeField] private TextMeshPro key;

    private PlayerAttack currentPlayerInArea;

    void Start()
    {
        SetUpStationsColors();
        
        Debug.Log(stationCureType.ToString() + activeColor.ToString() + inactiveColor.ToString());

        if (areaVisual != null)
        {
            for (int i = 0; i < areaVisual.Length; i++)
                areaVisual[i].color = inactiveColor;
            Debug.Log("InactiveColor: " + inactiveColor);
        }


    }

    private void SetUpStationsColors()
    {
        (activeColor, inactiveColor) = stationCureTypeClass.SetUpStationsColors(stationCureType);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentPlayerInArea = other.GetComponent<PlayerAttack>();
            currentPlayerInArea.SetCureSwitchArea(this);

            if (areaVisual != null)
            {
                for (int i = 0; i < areaVisual.Length; i++)
                    areaVisual[i].color = activeColor;
            }

            key.gameObject.SetActive(true);

            Debug.Log($"Player entered {stationCureType} area");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentPlayerInArea.SetCureSwitchArea(null);
            currentPlayerInArea = null;

            if (areaVisual != null)
            {
                for (int i = 0; i < areaVisual.Length; i++)
                    areaVisual[i].color = inactiveColor;
            }

            key.gameObject.SetActive(false);

            Debug.Log($"Player exited {stationCureType} area");
        }
    }

    public void TryChangeCure()
    {
        if (currentPlayerInArea != null)
        {
            currentPlayerInArea.SetCurrentCureType(stationCureType);
            SFXManager.Instance.PlayGlobalSound("GrabCure", 1f);
            Debug.Log($"Cure changed to {stationCureType}");

        }
    }
}