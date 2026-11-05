using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerUpgradeUI : MonoBehaviour
{
    [SerializeField]
    private Tower tower;
    public Tower Tower
    {
        get { return tower; }
        set { tower = value; }
    } // Publieke getter en setter voor de toren die wordt geüpgraded
    [SerializeField]
    private Button upgradeButton;
    public Button UpgradeButton
    {
        get { return upgradeButton; }
        set { upgradeButton = value; }
    } // Publieke getter voor de upgrade knop
    [SerializeField]
    private TextMeshProUGUI priceText;
    public TextMeshProUGUI PriceText
    {
        get { return priceText; }
        set { priceText = value; }
    } // Publieke getter voor het tekstveld dat de prijs toont

    private bool justOpened = true;
    
    private void Awake()
    {
        upgradeButton.onClick.AddListener(TryUpgrade);
    }
    private void TryUpgrade()
    {
        if (CoinManager.instance.Coins < tower.upgradeStages[tower.upgradeStage].Price) return;
        tower.Upgrade();

        Destroy(gameObject);
    }
    void Update()
    {
        if (justOpened)
        {
            justOpened = false;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

            Destroy(gameObject);
        }

        if (tower.upgradeStage >= tower.upgradeStages.Length)
        {
            Destroy(gameObject);
        }
    }
}
