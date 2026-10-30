using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8f;
    public Transform target;
    public GameObject hitPS;
    public AudioClip hitSFX;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        if (Vector3.Distance(transform.position, target.position) < 0.15f)
        {
            Enemy e = target.GetComponent<Enemy>();

            if (e != null)
            {
                e.health -= 1;

                if (e.health <= 0)
                {
                    // Haal het aantal munten op van deze specifieke vijand
                    CoinManager.instance.UpdateCoins(e.coinEarning);
                    Destroy(target.gameObject);
                }
            }
            Instantiate(hitPS, transform.position, Quaternion.identity);
            AudioManager.instance.PlaySFX(hitSFX);
            Destroy(gameObject);
        }
    }
}