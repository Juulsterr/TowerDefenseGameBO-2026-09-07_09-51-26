using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("Coin Data")]
    public int coins = 0;

    [Header("UI Reference")]
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        // Singleton patroon: zorgt dat er maar 1 CoinManager actief is
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Zet de startwaarde van de UI neer bij het opstarten
        UpdateCoins(0);
    }

    // Deze functie wordt aangeroepen vanuit Projectile.cs via:
    // CoinManager.instance.UpdateCoins(e.coinEarning);
    public void UpdateCoins(int changeAmount)
    {
        // Telt het verdiende bedrag van de enemy (e.coinEarning) op bij je saldo
        coins += changeAmount;

        // Veiligheidscheck: update de UI alleen als het tekstveld is toegewezen in de Inspector
        if (coinText != null)
        {
            coinText.text = coins.ToString();
        }
    }
}