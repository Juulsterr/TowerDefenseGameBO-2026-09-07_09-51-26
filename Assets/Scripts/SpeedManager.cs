using UnityEngine;
using UnityEngine.UI;

public class SpeedManager : MonoBehaviour
{
    private bool speedUp;
    public bool SpeedUp 
    { 
        get { return speedUp; } 
        set { speedUp = value; }
    } // Publieke getter en setter voor de snelheid van het spel
    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Image speedButtonImage;
    [SerializeField]
    private Color speedUpColor;

    public void ToggleSpeed()
    {
        speedUp = !speedUp;
        if (speedUp)
        {
            Time.timeScale = 2f;
            speedButtonImage.color = speedUpColor;
        }
        else
        {
            Time.timeScale = 1f;
            speedButtonImage.color = normalColor;
        }
    }
}
