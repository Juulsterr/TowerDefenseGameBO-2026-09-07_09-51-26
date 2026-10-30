using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 1;
    public int damage = 1;

    [Header("Reward")]
    public int coinEarning = 10; // Instelbaar via de Unity Inspector per enemy

    public Transform[] waypoints;
    public int currentWayPoint = 0;

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