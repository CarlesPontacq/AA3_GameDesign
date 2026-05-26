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
}
