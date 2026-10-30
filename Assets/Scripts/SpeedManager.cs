using UnityEngine;
using UnityEngine.UI;

public class SpeedManager : MonoBehaviour
{
    public bool speedUp;
    public Image speedButtonImage;
    public Color normalColor;
    public Color speedUpColor;

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
