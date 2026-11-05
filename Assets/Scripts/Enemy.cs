using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    public float Speed 
    { 
        get { return speed; }
        set { speed = value; }
    } // Publieke getter en setter voor de snelheid van de enemy

    [SerializeField]
    private int health = 1;
    public int Health 
    { 
        get { return health; } 
        set { health = value; }
    } // Publieke getter en setter voor de gezondheid van de enemy
    [SerializeField]
    private int damage = 1;
    public int Damage 
    { 
        get { return damage; } 
        set { damage = value; }
    } // Publieke getter en setter voor de schade die de enemy doet aan de speler
    [SerializeField]
    private int coinEarning = 10; // Instelbaar via de Unity Inspector per enemy
    public int CoinEarning 
    { 
        get { return coinEarning; } 
        set { coinEarning = value; }
    } // Publieke getter en setter voor het aantal munten dat de enemy oplevert

    [SerializeField]
    private Transform[] waypoints;
    public Transform[] Waypoints 
    { 
        get { return waypoints; } 
        set { waypoints = value; }
    } // Publieke getter en setter voor de waypoints van de enemy
    private int currentWayPoint = 0;
    public int CurrentWayPoint 
    { 
        get { return currentWayPoint; } 
        set { currentWayPoint = value; }
    } // Publieke getter en setter voor het huidige waypoint van de enemy

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[currentWayPoint];
        Vector3 dir = (target.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            currentWayPoint++;
            if (currentWayPoint >= waypoints.Length)
            {
                HealthManager.instance.UpdateHealth(-damage);
                Destroy(gameObject);
            }
        }
    }
}