using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

private int Health = 100;
public int health 
    { 
        get { return Health; } 
        set { Health = value; }
    }

public TextMeshProUGUI HealthText;

private void Awake()
    {
        instance = this;
        UpdateHealth(0);

    }

    public void UpdateHealth(int changeAmount)
    {
        Health += changeAmount;

        HealthText.text = Health.ToString();

        if (Health <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}
