using UnityEngine;

public class KamikazeEnemy : Enemy
{
    [Header("Kamikaze Instellingen")]
    public float explosionRadius = 0.5f; 

    [Header("Visuele Effecten")]
    public Sprite wreckageSprite;               // Sleep hier je getekende puin-sprite uit LibreSprite in!
    public GameObject explosionParticlePrefab;  // Sleep hier je wolkjes/explosie particle prefab in!
    public AudioClip bombSFX;

    private Transform targetTower;

    void Start()
    {
        FindNearestTower();
    }

    new void Update()
    {
        if (targetTower == null)
        {
            FindNearestTower();
            if (targetTower == null) return; 
        }

        Vector3 dir = (targetTower.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetTower.position) <= explosionRadius)
        {
            ExplodeAndLeaveWreckage();
        }
    }

    void FindNearestTower()
    {
        Tower[] allTowers = GameObject.FindObjectsOfType<Tower>();
        float shortestDistance = Mathf.Infinity;
        Transform nearest = null;

        foreach (Tower t in allTowers)
        {
            float distanceToTower = Vector3.Distance(transform.position, t.transform.position);
            if (distanceToTower < shortestDistance)
            {
                shortestDistance = distanceToTower;
                nearest = t.transform;
            }
        }

        targetTower = nearest;
    }

    void ExplodeAndLeaveWreckage()
    {
        if (targetTower != null)
        {
            // 1. Spawn het particle-effect (de explosie/wolkjes) op de plek van de toren
            if (explosionParticlePrefab != null)
            {
                Instantiate(explosionParticlePrefab, targetTower.position, Quaternion.identity);
            }

            // 2. Verander de sprite naar je getekende puin/hout stapeltje
            SpriteRenderer sr = targetTower.GetComponent<SpriteRenderer>();
            if (sr != null && wreckageSprite != null)
            {
                sr.sprite = wreckageSprite;
                sr.color = Color.white; // Zorg dat de kleur weer helder is (geen grijstint meer nodig)
            }

            // 3. Haal het Tower-script eraf zodat hij niet meer schiet of getarget kan worden
            Tower towerScript = targetTower.GetComponent<Tower>();
            if (towerScript != null)
            {
             AudioManager.instance.PlaySFX(bombSFX);
                Destroy(towerScript);
            }

            // De toren blijft op de grond staan als ruïne, inclusief collider, 
            // dus de speler kan hier géén nieuwe toren meer bouwen!
        }

        // Vernietig de kamikaze vijand zelf
        Destroy(gameObject);
    }
}