using UnityEngine;

public class CureTypesClass : MonoBehaviour
{
    [SerializeField] public Color activePurple = new Color(199f, 0f, 255f);
    [SerializeField] public Color inactivePurple = new Color(109f, 25f, 144f);
                     
    [SerializeField] public Color activeRed = new Color(255f, 0f, 0f);
    [SerializeField] public Color inactiveRed = new Color(128f, 0f, 0f);
                     
    [SerializeField] public Color activeBlue = new Color(0f, 255f, 0f);
    [SerializeField] public Color inactiveBlue = new Color(0f, 128f, 0f);
                     
    [SerializeField] public Color activeGreen = new Color(0f, 191f, 255f);
    [SerializeField] public Color inactiveGreen = new Color(0f, 104f, 128f);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public (Color, Color) SetUpStationsColors(CureType stationCureType)
    {
        Color activeColor = Color.white;
        Color inactiveColor = Color.gray;

        switch (stationCureType)
        {
        case CureType.CURE1:
            activeColor = activePurple;
            inactiveColor = inactivePurple;
            break;

        case CureType.CURE2:
            activeColor = activeRed;
            inactiveColor = inactiveRed;
            break;

        case CureType.CURE4:
            activeColor = activeGreen;
            inactiveColor = inactiveGreen;
            break;

        case CureType.CURE3:
            activeColor = activeBlue;
            inactiveColor = inactiveBlue;
            break;

        default:
            break;
        }

        return (activeColor, inactiveColor);    
    }

    public Color SetUpPlayerColors(CureType stationCureType)
    {
        Color activeColor = Color.white;

        switch (stationCureType)
        {
            case CureType.CURE1:
                activeColor = activePurple;
                break;

            case CureType.CURE2:
                activeColor = activeRed;
                break;

            case CureType.CURE4:
                activeColor = activeGreen;
                break;

            case CureType.CURE3:
                activeColor = activeBlue;
                break;

            default:
                break;
        }

        return activeColor;
    }
}
