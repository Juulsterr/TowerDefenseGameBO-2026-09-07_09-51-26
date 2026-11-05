using UnityEngine;

[System.Serializable]
public class TowerUpgradeStages
{
    [SerializeField]
    private float fireRange;
    public float FireRange 
    { 
        get { return fireRange; }  
    } // Publieke getter en setter voor het bereik van de toren
    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite 
    { 
        get { return sprite; } 
        set { sprite = value; }
    } // Publieke getter en setter voor de sprite van de toren
    [SerializeField]
    private int price;
    public int Price 
    { 
        get { return price; } 
        set { price = value; }
    } // Publieke getter en setter voor de prijs van de toren
    [SerializeField]
    private float range;
    public float Range 
    { 
        get { return range; } 
        set { range = value; }
    } // Publieke getter en setter voor het bereik van de toren
    [SerializeField]
    private float fireRate;
    public float FireRate 
    { 
        get { return fireRate; } 
        set { fireRate = value; }
    } // Publieke getter en setter voor de vuurtempo van de toren
}

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float range = 3f;
    public float Range 
    { 
        get { return range; } 
        set { range = value; }
    } // Publieke getter en setter voor het bereik van de toren
    [SerializeField]
    private float fireRate = 1f;
    public float FireRate 
    { 
        get { return fireRate; } 
        set { fireRate = value; }
    } // Publieke getter en setter voor het vuurtempo van de toren
    [SerializeField]
    private GameObject projectilePrefab;
    public GameObject ProjectilePrefab 
    { 
        get { return projectilePrefab; } 
    } // Publieke getter en setter voor het projectiel prefab van de toren
    [SerializeField]
    private Transform firePoint;
    public Transform FirePoint 
    { 
        get { return firePoint; } 
    } // Publieke getter en setter voor het vuurpunt van de toren

    [SerializeField]
    public TowerUpgradeStages[] upgradeStages;
    public TowerUpgradeStages[] UpgradeStages 
    { 
        get { return upgradeStages; }
    } // Publieke getter en setter voor de upgrade stages van de toren
    [SerializeField]
    public int upgradeStage = 0;
    public int UpgradeStage 
    { 
        get { return upgradeStage; } 
    } // Publieke getter en setter voor de huidige upgrade stage van de toren
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private GameObject towerUpgradeUIPrefab;
    public GameObject TowerUpgradeUIPrefab 
    { 
        get { return towerUpgradeUIPrefab; } 
    } // Publieke getter en setter voor het upgrade UI prefab van de toren
    [SerializeField]
    private GameObject currentUI;
    public GameObject CurrentUI 
    { 
        get { return currentUI; } 
        set { currentUI = value; }
    } // Publieke getter en setter voor het huidige upgrade UI van de toren

    [SerializeField]
    private int towerPrice = 1;
    public int TowerPrice 
    { 
        get { return towerPrice; } 
        set { towerPrice = value; }
    } // Publieke getter en setter voor de prijs van de toren
    [SerializeField]
    private float fireCooldown = 0f;
    public float FireCooldown 
    { 
        get { return fireCooldown; } 
        set { fireCooldown = value; }
    } // Publieke getter en setter voor de cooldown van de toren

    [SerializeField]
    private GameObject cloudPS;
    public GameObject CloudPS 
    { 
        get { return cloudPS; } 
        set { cloudPS = value; }
    } // Publieke getter en setter voor het wolkjes particle systeem van de toren

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Instantiate(cloudPS, transform.position, Quaternion.identity);

    }
    void Update()
    {
        fireCooldown -= Time.deltaTime;

        Enemy target = FindBestTarget();

        if(target != null && fireCooldown <= 0f)
        {
            Shoot(target);
            fireCooldown = 1f / fireRate;
        }
    }

    Enemy FindBestTarget()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        Enemy best = null;
        float bestProgress = -1f;

        foreach(Enemy e in enemies)
        {
            float dist = Vector2.Distance(transform.position, e.transform.position);

            if(dist <= range)
            {
                if(e.CurrentWayPoint > bestProgress)
                {
                    bestProgress = e.CurrentWayPoint;
                    best = e;
                }
            }
        }
        return best;
    }
    void Shoot(Enemy target)
    {
        GameObject p = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile pr = p.GetComponent<Projectile>();
        pr.Target = target.transform;
    }

    public void Upgrade()
    {
        TowerUpgradeStages currentUpgradeStage = upgradeStages[upgradeStage];

        range = currentUpgradeStage.Range;
        fireRate = currentUpgradeStage.FireRate;
        sr.sprite = currentUpgradeStage.Sprite;
        CoinManager.instance.UpdateCoins(-currentUpgradeStage.Price);
        upgradeStage += 1;
    }

    private void OnMouseDown()
    {
        if(currentUI == null)
        {
            currentUI = Instantiate(towerUpgradeUIPrefab, FindObjectOfType<Canvas>().transform);
        }

        TowerUpgradeUI currentUpgradeUI = currentUI.GetComponent<TowerUpgradeUI>();
        currentUpgradeUI .Tower = this;

        currentUI.transform.position = Input.mousePosition + new Vector3(50, -50);

        if (upgradeStage >= upgradeStages.Length) return;
        currentUpgradeUI.PriceText.text = upgradeStages[upgradeStage].Price.ToString();
    }
}