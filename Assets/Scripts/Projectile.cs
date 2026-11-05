using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;
    public float Speed 
    { 
        get { return speed; } 
        set { speed = value; }
    } // Publieke getter en setter voor de snelheid van het projectiel

    [SerializeField]
    private Transform target;
    public Transform Target 
    { 
        get { return target; } 
        set { target = value; }
    } // Publieke getter en setter voor het doelwit van het projectiel
    [SerializeField]
    private AudioClip hitSFX;
    [SerializeField]
    private GameObject hitPS;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * Speed * Time.deltaTime;

        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        if (Vector3.Distance(transform.position, target.position) < 0.15f)
        {
            Enemy e = target.GetComponent<Enemy>();

            if (e != null)
            {
                e.Health -= 1;

                if (e.Health <= 0)
                {
                    // Haal het aantal munten op van deze specifieke vijand
                    CoinManager.instance.UpdateCoins(e.CoinEarning);
                    Destroy(target.gameObject);
                }
            }
            Instantiate(hitPS, transform.position, Quaternion.identity);
            AudioManager.instance.PlaySFX(hitSFX);
            Destroy(gameObject);
        }
    }
}